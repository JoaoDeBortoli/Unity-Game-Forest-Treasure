using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBrick : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("Falling", fallingTime);
        }
    }

    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if(collider.gameObject.layer == 8)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    void Falling()
    {
        anim.SetTrigger("destruir");
        boxColl.isTrigger = true;
        Destroy(gameObject, 1f);
    }
}
