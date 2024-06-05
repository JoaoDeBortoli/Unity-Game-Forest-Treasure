using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public float lifetime;    // Tempo de vida da flecha em segundos

    private void Start()
    {
        // Destroi a flecha após 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Movimenta a flecha para frente
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Causa dano ao jogador
            //collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            // Destroi a flecha
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            // Destroi a flecha se colidir com um obstáculo
            Destroy(gameObject);
        }
    }
}
