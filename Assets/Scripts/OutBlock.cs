using UnityEngine;
using System.Collections;

public class OutBlock : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public IEnumerator Out()
    {
        anim.SetInteger("Out", 1);

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    public IEnumerator In()
    {
        anim.SetInteger("In", 1);

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
