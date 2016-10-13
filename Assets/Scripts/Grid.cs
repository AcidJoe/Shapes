using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject moveBlock;
    public GameObject outBlock;

    public Collider2D col;

    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;
    public GameObject t5;
    public GameObject t6;

    public GameObject actCol;

    public Cell[] cells;

    public Cell first;
    public Cell second;

    public Cell currentCell;
    public Cell currentCheck;

    public List<Cell> newCells;
    public List<Cell> forChange;
    public MovingBlock[] movingBlocks;

    public bool isCheck;
    public bool isMove;
    public bool isEnd;
    public static bool isAnimation;

    ChangeCursor cursor;

    void Start()
    {
        cursor = FindObjectOfType<ChangeCursor>();

        isEnd = false;
        isMove = false;
        isCheck = false;
        newCells = new List<Cell>();
        forChange = new List<Cell>();

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Instantiate(cellPrefab, new Vector3(x, y), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        cells = FindObjectsOfType<Cell>();
        movingBlocks = FindObjectsOfType<MovingBlock>();

        if (isMove)
        {
            if (movingBlocks.Length == 1)
            {
                movingBlocks[0]._destroy();
            }
            if (movingBlocks.Length <= 0)
            {
                isMove = false;
            }
        }
    }

    public bool CheckPosition(Block[] blocks)
    {
        bool one = false, two = false;

        foreach (Cell c in cells)
        {
            if (Mathf.Abs(c.transform.position.x - blocks[0].transform.position.x) < 0.5f && Mathf.Abs(c.transform.position.y - blocks[0].transform.position.y) < 0.5f)
            {
                if (c.number < 1)
                {
                    first = c;
                    one = true;
                }
                else
                {
                    one = false;
                }
            }
            else if (blocks.Length > 1)
            {
                if (Mathf.Abs(c.transform.position.x - blocks[1].transform.position.x) < 0.5f && Mathf.Abs(c.transform.position.y - blocks[1].transform.position.y) < 0.5f)
                {
                    if (c.number < 1)
                    {
                        second = c;
                        two = true;
                    }
                    else
                    {
                        two = false;
                    }
                }
            }
            else if (blocks.Length == 1)
            {
                two = true;
            }
        }
        if (one == two)
        {
            newCells.Clear();

            first.SetNumber(blocks[0].number);
            //first.isNew = true;
            newCells.Add(first);
            blocks[0].MoveToCell(first.transform.position);
            if (blocks.Length > 1)
            {
                second.SetNumber(blocks[1].number);
                //second.isNew = true;
                newCells.Add(second);
                blocks[1].MoveToCell(second.transform.position);
            }
            return true;
        }
        return false;
    }

    public void CheckMatches()
    {
        isCheck = true;

        if (newCells.Count > 0)
        {
            currentCell = GetMinNumber();

            List<Cell> currentMatch = CheckChain(currentCell);

            if (currentMatch.Count >= 3)
            {
                StartCoroutine(VisualizeChain(currentMatch));
            }
            else
            {
                newCells.Remove(currentCell);
                CheckMatches();
            }
        }
        else
        {
            isCheck = false;
        }
    }

    List<Cell> CheckChain(Cell cell)
    {
        List<Cell> chain = new List<Cell>();
        List<Cell> needCheck = new List<Cell>();
        List<Cell> check = new List<Cell>();

        chain.Add(cell);

        foreach (Cell c in cell.neighbours())
        {
            if (c.number == cell.number)
            {
                needCheck.Add(c);
            }
        }

        while (needCheck.Count > 0)
        {
            check.Clear();

            foreach (Cell c in needCheck)
            {
                chain.Add(c);

                foreach (Cell cl in c.neighbours())
                {
                    if (cl.number == cell.number)
                    {
                        check.Add(cl);
                    }
                }
            }

            needCheck.Clear();

            foreach (Cell c in check)
            {
                if (!chain.Contains(c))
                    needCheck.Add(c);
            }
        }

        return chain;
    }

    Cell GetMinNumber()
    {
        Cell c = null;
        int curNum = 10;

        foreach (Cell cell in newCells)
        {
            if (cell.number < curNum)
            {
                curNum = cell.number;
                c = cell;
            }
        }

        return c;
    }

    public bool isHaveDoubles()
    {
        bool x = false;

        foreach (Cell c in cells)
        {
            if (c.number == 0)
            {
                foreach (Cell s in c.neighbours())
                {
                    if (s.number == 0)
                    {
                        x = true;
                        break;
                    }
                }
            }
        }

        return x;
    }

    public int getMax()
    {
        int i = Manager.gridMax;

        foreach (Cell c in cells)
        {
            if (c.number + 1 > i)
            {
                i = c.number + 1;
            }
        }
        return i;
    }

    public IEnumerator VisualizeChain(List<Cell> chain)
    {
        isMove = true;
        Game.matches++;

        foreach (Cell c in chain)
        {
            if (c != currentCell)
            {
                GameObject m = Instantiate(moveBlock, c.transform.position, Quaternion.identity) as GameObject;
                m.GetComponent<Block>().number = c.number;
                m.GetComponent<MovingBlock>().SetTarget(currentCell.transform);
                c.SetNumber(0);
            }
            else if (c == currentCell)
            {
                GameObject m = Instantiate(moveBlock, c.transform.position, Quaternion.identity) as GameObject;
                Instantiate(actCol, c.transform.position, Quaternion.identity);
                m.GetComponent<Block>().number = c.number;
                m.GetComponent<SpriteRenderer>().sortingOrder = 20;
                m.GetComponent<MovingBlock>().number.sortingOrder = 25;
                Game.shapes[c.number - 1]++;
            }
        }

        yield return new WaitWhile(() => isMove == true);

        isAnimation = true;

        int i = currentCell.number + 1;
        if (i > 7)
        {
            StartCoroutine(Star(currentCell));
            i = 0;
        }
        currentCell.SetNumber(i);

        Destroy(GameObject.FindGameObjectWithTag("ActiveCol"));

        GameObject g = Instantiate(_animation(), currentCell.transform.position, Quaternion.identity) as GameObject;

        yield return new WaitForSeconds(1);

        isAnimation = false;
        Destroy(g);

        yield return new WaitWhile(() => isAnimation == true);

        foreach (Cell c in cells)
        {
            if (c.number == 0 && newCells.Contains(c))
            {
                newCells.Remove(c);
            }
        }

        CheckMatches();
    }

    public GameObject _animation()
    {
        switch (currentCell.number -1)
        {
            case 1:
                return t1;
            case 2:
                return t2;
            case 3:
                return t3;
            case 4:
                return t4;
            case 5:
                return t5;
            case 6:
                return t6;
        }

        return t1;
    }

    public GameObject _animation(int number)
    {
        switch (number)
        {
            case 1:
                return t1;
            case 2:
                return t2;
            case 3:
                return t3;
            case 4:
                return t4;
            case 5:
                return t5;
            case 6:
                return t6;
        }

        return t1;
    }

    public IEnumerator Star(Cell c)
    {
        yield return new WaitWhile(() => isMove == true);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                foreach (Cell cell in cells)
                {
                    if (cell.transform.position == new Vector3(c.transform.position.x + x, c.transform.position.y + y))
                    {
                        cell.SetNumber(0);
                    }
                }
            }
        }
    }

    public void ChangeCells(Cell c)
    {
        forChange.Add(c);

        if(forChange.Count == 2)
        {
            forChange[0].Pay();
            StartCoroutine(change());
        }
    }

    public IEnumerator change()
    {
        int restore1 = forChange[0].number;
        int restore0 = forChange[1].number;

        forChange[0].SetNumber(0);
        forChange[1].SetNumber(0);

        GameObject m = Instantiate(outBlock, forChange[0].pos, Quaternion.identity) as GameObject;
        GameObject m1 = Instantiate(outBlock, forChange[1].pos, Quaternion.identity) as GameObject;
        m.GetComponent<Block>().number = restore1;
        m1.GetComponent<Block>().number = restore0;
        StartCoroutine(m.GetComponent<OutBlock>().Out());
        StartCoroutine(m1.GetComponent<OutBlock>().Out());

        yield return new WaitForSeconds(1);

        GameObject q = Instantiate(outBlock, forChange[0].pos, Quaternion.identity) as GameObject;
        GameObject q1 = Instantiate(outBlock, forChange[1].pos, Quaternion.identity) as GameObject;
        q.GetComponent<Block>().number = restore0;
        q1.GetComponent<Block>().number = restore1;
        StartCoroutine(q.GetComponent<OutBlock>().In());
        StartCoroutine(q1.GetComponent<OutBlock>().In());

        yield return new WaitForSeconds(1);

        forChange[0].SetNumber(restore0);
        forChange[1].SetNumber(restore1);

        foreach (Cell cell in forChange)
        {
            newCells.Add(cell);
            cell.isReadyToChange = false;
        }

        cursor.SetCursorState(ChangeCursor.State.regular);
        CheckMatches();
    }

    public void ActivateCollider(int i)
    {
        switch (i)
        {
            case 0:
                col.enabled = false;
                break;
            case 1:
                col.enabled = true;
                break;
        }
    }

    public bool CheckPossible()
    {
        int count = 0;

        foreach(Cell c in cells)
        {
            if(c.number != 0)
            {
                count++;
            }
        }

        if(count == 25)
        {
            return false;
        }

        return true;
    }

    public void Clear(Vector3 pos, int number)
    {
        GameObject m = Instantiate(outBlock, pos, Quaternion.identity) as GameObject;
        m.GetComponent<Block>().number = number;
        StartCoroutine(m.GetComponent<OutBlock>().Out());
    }

    public IEnumerator Up(Vector3 pos, int number)
    {
        GameObject g = Instantiate(_animation(number), pos, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(1);
        Destroy(g);
    }
}