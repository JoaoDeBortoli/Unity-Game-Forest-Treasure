using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float horizontal;
    private bool isFacingRight;
    private bool doubleJump = true;

    private bool canDash = true;
    private bool isDashing;
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    private TrailRenderer tr;
    public GameObject blood;


    public float speed;
    public float jumpPower;

    [Header("Audios")]
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip deathSound;

    [Header("Box de colisão com chão")]
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing)
        {   
            return;
        }
        Move();
        Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    // gira a sprit do personagem
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal != 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if(!isFacingRight && horizontal > 0)
        {
            Flip();
        } else if(isFacingRight && horizontal < 0)
        {
            Flip();
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                SoundManager.sons.PlaySound(jumpSound);
            } else if(doubleJump)
            {
                anim.SetTrigger("doublejump");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                SoundManager.sons.PlaySound(jumpSound);
                doubleJump = false;
            }
        }
        if(isGrounded())
        {
            doubleJump = true;
        }
        anim.SetBool("jump", !isGrounded());
    }

    //verifica se esta tocando no chao
    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        SoundManager.sons.PlaySound(dashSound);
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // desesnha are de colisao dos pes
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    // verifica colisao com objetos "sem colisao"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

    //verifica colisao com objetos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

    public void Death()
    {
        Instantiate (blood, transform.position, transform.rotation);
        SoundManager.sons.PlaySound(deathSound);
        gameObject.SetActive(false);
        GameController.instance.GameOver();
    }
}
