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

    void Start ()
    {
        gridMax = 3;
        spawnPoint = new Vector3(2, -1.7f);
        cursor = FindObjectOfType<ChangeCursor>();
    }
	
	void Update ()
    {
        grid = FindObjectOfType<Grid>();
        blocks = FindObjectsOfType<Block>();

        if(blocks.Length < 1 && !grid.isCheck)
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
        Destroy(currentBlock);
    }

    public void UpShape()
    {
        cursor.changeState(2);
    }

    public void Clear()
    {
        cursor.changeState(3);
    }

    public void Change()
    {
        cursor.changeState(1);
    }
}
