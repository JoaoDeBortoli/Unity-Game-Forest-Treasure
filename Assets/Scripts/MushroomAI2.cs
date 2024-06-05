using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAI2 : MonoBehaviour
{
    
    public float attackRange;
    public float attackCooldown;
    private Transform player;

    private float nextAttackTime;


    public GameObject bullet;
    public float bulletSpeed;

    private Animator anim;
    public AudioClip shotsSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            if(player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + attackCooldown;
            }
        }

    }

    IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        Shoot();
        SoundManager.sons.PlaySound(shotsSound);
        yield return new WaitForSeconds(1); // Duração do ataque, tempo da animação

    }
    void Shoot()
    {
        GameObject Bullet = Instantiate(bullet, transform.position, transform.rotation);
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
