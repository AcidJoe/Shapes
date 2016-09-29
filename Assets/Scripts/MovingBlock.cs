using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingBlock : MonoBehaviour
{
    public Block block;
    Transform target;

    Transform destination;

    public List<Transform> path = new List<Transform>();

    public SpriteRenderer number;

    void Awake()
    {
        block = GetComponent<Block>();
    }

    void Update()
    {
        if (target && destination)
        {
            transform.Translate((destination.position - transform.position).normalized * Time.deltaTime);
            _destroy();
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void SetDestination(Transform d)
    {
        destination = d;

        if(destination == target)
        {
            destination = target;
        }
    }

    void _destroy()
    {
        if(Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            Destroy(gameObject);
        }
        else if(Vector3.Distance(transform.position, destination.position) < 0.01f)
        {
            destination = target;
        }
    }
}
