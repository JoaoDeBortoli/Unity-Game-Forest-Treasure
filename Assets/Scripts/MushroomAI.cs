using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public float attackRange;
    public float attackCooldown;
    private Transform player;
    private int currentPatrolIndex;
    private bool isPatrolling;
  
    private float nextAttackTime;


    public GameObject bullet;
    public float bulletSpeed;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPatrolIndex = 0;
        isPatrolling = true;
    }

    void Update()
    {
        if (isPatrolling)
        {
            Patrol();
        }
        
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + attackCooldown;
            }
        }

    }
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            Flip();
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    IEnumerator Attack()
    {
        isPatrolling = false;


        // Aqui você pode adicionar animações ou efeitos de ataque
        Shoot();
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(1); // Duração do ataque, tempo da animação
        isPatrolling = true;
    }
    void Shoot()
    {
        GameObject Bullet = Instantiate(bullet, transform.position, transform.rotation);
        Vector2 direction = (player.position - transform.position).normalized;
        Bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
