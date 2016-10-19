using UnityEngine;
using System.Collections;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D regular;
    public Texture2D change;
    public Texture2D up;
    public Texture2D clear;
    
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public enum State { regular, change, up, clear}

    public State currentState;

    public bool isSpecialState;

    public Grid grid;
    public UIManager ui;

    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        grid = FindObjectOfType<Grid>();
        SetCursorState(State.regular);
    }

    public void SetCursorState(State s)
    {
        switch (s)
        {
            case State.regular:
                isSpecialState = false;
                grid.ActivateCollider(1);
                foreach(Cell c in grid.forChange)
                {
                    c.isReadyToChange = false;
                }
                grid.forChange.Clear();
                ui.activeSkill(0);
                Cursor.SetCursor(regular, hotSpot, cursorMode);
                break;
            case State.change:
                isSpecialState = true;
                grid.ActivateCollider(0);
                ui.activeSkill(1);
                Cursor.SetCursor(change, hotSpot, cursorMode);
                break;
            case State.up:
                isSpecialState = true;
                grid.ActivateCollider(0);
                ui.activeSkill(2);
                Cursor.SetCursor(up, hotSpot, cursorMode);
                break;
            case State.clear:
                isSpecialState = true;
                grid.ActivateCollider(0);
                ui.activeSkill(3);
                Cursor.SetCursor(clear, hotSpot, cursorMode);
                break;
        }
        currentState = s;
    }

    public void changeState(int i)
    {
        switch (i)
        {
            case 0:
                SetCursorState(State.regular);
                break;
            case 1:
                SetCursorState(State.change);
                break;
            case 2:
                SetCursorState(State.up);
                break;
            case 3:
                SetCursorState(State.clear);
                break;
        }
    }
}
