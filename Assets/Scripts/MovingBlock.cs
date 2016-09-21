using UnityEngine;
using System.Collections;

public class MovingBlock : MonoBehaviour
{
    public Block block;
    Transform target;

    public SpriteRenderer number;

    void Awake()
    {
        block = GetComponent<Block>();
    }

	void Update ()
    {
        if (target)
        {
            transform.Translate((target.position - transform.position).normalized * Time.deltaTime);
            _destroy();
        }
	}

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void _destroy()
    {
        if(Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
