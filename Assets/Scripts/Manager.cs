using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    public static int gridMax;

    public GameObject single_block;
    public GameObject double_block;

    public Block[] blocks;

    public GameObject currentBlock;

    public Vector3 spawnPoint;

    public Grid grid;

    public ChangeCursor cursor;

    public UIManager ui;

    void Start()
    {
        gridMax = 3;
        spawnPoint = new Vector3(2, -1.7f);
        cursor = FindObjectOfType<ChangeCursor>();
        ui = GetComponent<UIManager>();
    }

    void Update()
    {
        grid = FindObjectOfType<Grid>();
        blocks = FindObjectsOfType<Block>();

        if (!grid.CheckPossible())
        {
            grid.isEnd = true;
            GameOver();
        }

        if (blocks.Length < 1 && !grid.isCheck && !grid.isEnd)
        {
            SpawnBlock();
        }

        if (cursor.isSpecialState)
        {
            if (Input.GetMouseButtonDown(1))
            {
                cursor.changeState(0);
            }
        }
    }

    void SpawnBlock()
    {
        int ran = Random.Range(0, 10);

        gridMax = grid.getMax();

        if (grid.isHaveDoubles())
        {

            if (ran <= 2)
            {
                currentBlock = Instantiate(single_block, spawnPoint, Quaternion.identity) as GameObject;
            }
            else
            {
                currentBlock = Instantiate(double_block, spawnPoint, Quaternion.identity) as GameObject;
            }
        }
        else
        {
            currentBlock = Instantiate(single_block, spawnPoint, Quaternion.identity) as GameObject;
        }
    }

    public void Remove()
    {
        CheckRemove();
    }

    public void UpShape()
    {
        Check(2);
    }

    public void Clear()
    {
        Check(3);
    }

    public void Change()
    {
        Check(1);
    }

    bool CheckCost()
    {
        if (Game.player.money - Game.specialCost >= 0)
        {
            return true;
        }

        return false;
    }

    void Check(int i)
    {
        int count = 0;
        foreach (Cell c in grid.cells)
        {
            if (c.number > 0)
            {
                count++;
            }
        }

        if (count >= 2)
        {
            if (CheckCost())
            {
                cursor.changeState(i);
                //Game.Pay();
                //Game.IncreaseCost();
            }
            else
            {
                StartCoroutine(ui.Error(1));
            }
        }
        else
        {
            StartCoroutine(ui.Error(2));
        }
    }

    void CheckRemove()
    {
        if (CheckCost())
        {
            Destroy(currentBlock);
            Game.Pay();
            Game.IncreaseCost();
        }
        else
        {
            StartCoroutine(ui.Error(1));
        }
    }

    void GameOver()
    {
        foreach(Cell c in grid.cells)
        {
            c.gameObject.SetActive(false);
        }
        ui.GameOver();
    }

    public void Return()
    {
        Game.player.money += Game.MoneyGet();
        Game.GoToScene(0);
    }
}
