﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingBlock : MonoBehaviour
{
    public Block block;
    Transform target;

    public List<Transform> path = new List<Transform>();

    public SpriteRenderer number;
    public GameObject border;

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
            fadeShape();

            if (dist > 0.5)
            {
                Quaternion rot = Quaternion.LookRotation(target.position - ps.transform.position);
                ps.transform.rotation = rot;
            }
            if (!isSet)
            {
                Setup();
            }

            var sh = ps.shape;
            sh.radius = dist / 2;

            transform.localScale -= new Vector3(Time.deltaTime * 0.5f, Time.deltaTime * 0.5f);
            transform.Translate((target.position - transform.position) * Time.deltaTime * 0.8f);

            if(dist < 0.2)
            {
                Destroy(gameObject);
            }
        }
    }

    public void fadeShape()
    {
        block.spriterNum.color = Color.Lerp(block.spriterNum.color, block.spriterBlock.color, 0.2f);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void Setup()
    {
        border.SetActive(false);
        ps.startColor = block.spriterBlock.color;

        ps.Play(true);
        isSet = true;
    }

    public void _destroy()
    {
        Destroy(gameObject);
    }
}
