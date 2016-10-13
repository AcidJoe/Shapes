using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : ColorElement
{
    public Grid grid;
    public ChangeCursor cursor;

    public GameObject roll;

    public Collider2D col;
    //public bool isNew;
    public Vector3 pos;

    public bool isReadyToChange = false;

	void Awake ()
    {
        roll.SetActive(false);
        col = GetComponent<Collider2D>();
        cursor = FindObjectOfType<ChangeCursor>();
        pos = transform.position;
        grid = FindObjectOfType<Grid>();
        //isNew = false;
        isSet = false;
	}
	
	void Update ()
    {
        if (!isSet)
        {
            SetUp();
        }

        if (cursor.isSpecialState)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }

        if (isReadyToChange)
        {
            roll.SetActive(true);
        }
        else
        {
            roll.SetActive(false);
        }
	}

    public void SetNumber(int i)
    {
        number = i;
        isSet = false;
    }

    public List<Cell> neighbours()
    {
        List<Cell> find = new List<Cell>();

        foreach(Cell s in grid.cells)
        {
            if(s.pos.x == pos.x + 1 && s.pos.y == pos.y)
            {
                find.Add(s);
            }
            else if (s.pos.x == pos.x - 1 && s.pos.y == pos.y)
            {
                find.Add(s);
            }
            else if (s.pos.x == pos.x && s.pos.y == pos.y + 1)
            {
                find.Add(s);
            }
            else if (s.pos.x == pos.x && s.pos.y == pos.y -1)
            {
                find.Add(s);
            }
        }

        return find;
    }

    void OnMouseDown()
    {
        switch (cursor.currentState)
        {
            case ChangeCursor.State.regular:
                break;
            case ChangeCursor.State.change:
                Action(1);
                break;
            case ChangeCursor.State.up:
                Action(2);
                break;
            case ChangeCursor.State.clear:
                Action(3);
                break;
        }
    }

    public void Action(int i)
    {
        switch (i)
        {
            case 1:
                Change();
                break;
            case 2:
                StartCoroutine(Up());
                Pay();
                break;
            case 3:
                StartCoroutine(Clear());
                Pay();
                break;
        }
    }

    public void Pay()
    {
        Game.Pay();
        Game.IncreaseCost();
    }

    public IEnumerator Up()
    {
        if (number != 0)
        {
            int i = number + 1;
            bool valid;

            if (i > 7)
            {
                i = 7;
                valid = false;
            }
            else
            {
                valid = true;
            }

            StartCoroutine(grid.Up(pos, number));
            isReadyToChange = true;
            yield return new WaitForSeconds(1);
            isReadyToChange = false;
            if (valid)
            {
                SetNumber(i);
                grid.newCells.Add(this);
                grid.CheckMatches();

                Break();
            }
        }
    }

    public void Change()
    {
        if (number != 0)
        {
            if (!isReadyToChange)
            {
                isReadyToChange = true;
                grid.ChangeCells(this);
            }
            else if (isReadyToChange && grid.forChange.Contains(this))
            {
                isReadyToChange = false;
                grid.forChange.Remove(this);
            }
        }
    }

    public IEnumerator Clear()
    {
        if (number != 0)
        {
            grid.Clear(pos, number);
            SetNumber(0);
            Break();
        }
        isReadyToChange = true;
        yield return new WaitForSeconds(1);
        isReadyToChange = false;
    }

    void Break()
    {
        cursor.SetCursorState(ChangeCursor.State.regular);
    }
}
