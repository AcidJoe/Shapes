using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingBlock : MonoBehaviour
{
    public Block block;
    Transform target;

    public List<Transform> path = new List<Transform>();

    public SpriteRenderer number;

    public GameObject particles;
    public ParticleSystem ps;

    public Transform dot1, dot2;
    public float dist;

    public bool isSet;

    void Awake()
    {
        isSet = false;
        block = GetComponent<Block>();
        ps = particles.GetComponent<ParticleSystem>();
        ps.Pause(true);
    }

    void Update()
    {
        dist = Vector3.Distance(dot1.position, dot2.position);

        if (target)
        {
            if (!isSet)
            {
                Setup();
            }

            ps.Play(true);
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void Setup()
    {
        print(block.spriterBlock.color);
        ps.startColor = block.spriterBlock.color;
        print(ps.startColor);
        isSet = true;
    }

    void _destroy()
    {

    }
}
