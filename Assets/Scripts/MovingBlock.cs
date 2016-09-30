using UnityEngine;
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
        ps.Pause();
    }

    void Update()
    {
        dist = Vector3.Distance(dot1.position, dot2.position);

        if (target)
        {
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

            transform.localScale -= new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f);

            if(dist < 0.2)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void Setup()
    {
        border.SetActive(false);
        ps.startColor = block.spriterBlock.color;
        var sh = ps.shape;
        sh.radius = dist/2;
        ps.Play();
        isSet = true;
    }

    public void _destroy()
    {
        Destroy(gameObject);
    }
}
