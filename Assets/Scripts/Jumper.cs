using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpPower;
    public AudioClip jumperSound;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
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
            anim.SetTrigger("jump");
            SoundManager.sons.PlaySound(jumperSound);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
        }
    }
}
