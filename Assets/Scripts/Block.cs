using UnityEngine;
using System.Collections;

public class Block : ColorElement
{
    public bool isReady;

	void Awake ()
    {
        isSet = false;
	}
	
	void Update ()
    {
        if (!isSet && number != 0)
        {
            SetUp();
        }
	}

    public void MoveToCell(Vector3 destination)
    {
        transform.position = destination;
    }
}
