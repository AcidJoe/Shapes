using UnityEngine;
using System.Collections;

public class ColorElement : MonoBehaviour
{
    public int number = 0;

    public SpriteRenderer spriterBlock;
    public SpriteRenderer spriterNum;

    public Color[] colors;
    public Sprite[] nums;
    public Color[] numColors;

    protected bool isSet;

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    protected void SetUp()
    {
        spriterBlock.color = colors[number];
        spriterNum.sprite = nums[number];
        spriterNum.color = numColors[number];
        isSet = true;
    }
}
