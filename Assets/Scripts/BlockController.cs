using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour
{
    public Block[] blocks;

    public Vector3 mousePos;

    public Vector3 origin;

    public float delay;
    public float moveReadyTime;

    public Vector3[] positions;
    public int cur0, cur1;

    public bool isOverTheGrid;

    public Grid grid;
    public ChangeCursor cursor;

    void Awake()
    {
        cursor = FindObjectOfType<ChangeCursor>();

        cur0 = 0;
        cur1 = 2;
        blocks = GetComponentsInChildren<Block>();
        origin = transform.position;

        grid = FindObjectOfType<Grid>();

        if (blocks.Length > 1)
        {
            blocks[0].transform.localPosition = positions[cur0];
            blocks[1].transform.localPosition = positions[cur1];
        }

        isOverTheGrid = false;

        if (blocks.Length == 2)
        {
            int ran1, ran2;

            ran1 = Random.Range(1, Manager.gridMax);
            ran2 = Random.Range(1, Manager.gridMax);
            while (ran1 == ran2)
            {
                ran2 = Random.Range(1, Manager.gridMax);
            }

            blocks[0].number = ran1;
            blocks[1].number = ran2;
        }
        else
        {
            blocks[0].number = Random.Range(1, Manager.gridMax);
        }
    }

    void Update()
    {
        moveReadyTime += Time.deltaTime;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        isOverTheGrid = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        isOverTheGrid = false;
    }

    void OnMouseDown()
    {
        moveReadyTime = 0;
    }

    void OnMouseDrag()
    {
        if (moveReadyTime > delay && !cursor.isSpecialState)
        {
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        if (!cursor.isSpecialState)
        {
            if (moveReadyTime < delay)
            {
                Reverse();
            }
            else
            {
                if (isOverTheGrid)
                {
                    if (grid.CheckPosition(blocks))
                    {
                        Destroy(gameObject);
                        grid.CheckMatches();
                    }
                    else
                    {
                        transform.position = origin;
                    }
                }
                else
                {
                    transform.position = origin;
                }
            }
        }
    }

    void Reverse()
    {
        if(blocks.Length > 1)
        {
            cur0 = next(cur0);
            cur1 = next(cur1);

            blocks[0].transform.localPosition = positions[cur0];
            blocks[1].transform.localPosition = positions[cur1];
        }
    }

    int next(int x)
    {
        int i = x + 1;

        if(i != 4)
        {
            return i;
        }
        else if(i == 4)
        {
            i = 0;
            return i;
        }

        return i;
    }
}
