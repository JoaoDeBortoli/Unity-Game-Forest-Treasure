using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidManAI : MonoBehaviour
{
    public float speed;
    public float attackRange;
    private float speedInic;
    public Transform point1;
    public Transform point2;
    private Transform player;

    private Vector3 target;

    void Start()
    {
        speedInic = speed;
        target = point1.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            speed = 3;
        }
        else
        {
            speed = speedInic;
        }
        Move();
    }

    void Move()
    {
        // move o inimigo
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Checa se o inimigo chegou no ponto de destino
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            // troca o target entre os 2 pontos
            Flip();
            target = (target == point1.position) ? point2.position : point1.position;
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
