using UnityEngine;
using System.Collections;

public class Tunel : MonoBehaviour
{
    SpriteRenderer color;

    void Awake()
    {
        color = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color col)
    {
        color.color = col;
    }
}
