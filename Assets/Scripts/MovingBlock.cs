using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingBlock : MonoBehaviour
{
    public Block block;
    Transform target;

    public GameObject tunel;

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

        //GameObject tun;

        //if (transform.position.x != d.position.x)
        //{
        //    if (transform.position.x > d.position.x)
        //    {
        //        tun = Instantiate(tunel, new Vector3(d.position.x + .5f, d.position.y), Quaternion.identity) as GameObject;
        //        //tun.GetComponent<Tunel>().SetColor(block.colors[block.number]);
        //    }
        //    else if (transform.position.x < d.position.x)
        //    {
        //        tun = Instantiate(tunel, new Vector3(d.position.x - .5f, d.position.y), Quaternion.identity) as GameObject;
        //        //tun.GetComponent<Tunel>().SetColor(block.colors[block.number]);
        //    }
        //}
        //else if (transform.position.y != d.position.y)
        //{
        //    if (transform.position.y > d.position.y)
        //    {
        //        tun = Instantiate(tunel, new Vector3(d.position.x, d.position.y + .5f), Quaternion.Euler(0, 0, 90)) as GameObject;
        //        //tun.GetComponent<Tunel>().SetColor(block.colors[block.number]);
        //    }
        //    else if (transform.position.y < d.position.y)
        //    {
        //        tun = Instantiate(tunel, new Vector3(d.position.x, d.position.y - .5f), Quaternion.Euler(0, 0, 90)) as GameObject;
        //        //tun.GetComponent<Tunel>().SetColor(block.colors[block.number]);
        //    }
        //}

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
