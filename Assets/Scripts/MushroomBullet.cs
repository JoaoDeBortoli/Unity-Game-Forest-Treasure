using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBullet : MonoBehaviour
{
    public float lifetime;    // Tempo de vida da flecha em segundos

    private void Start()
    {
        // Destroi a flecha após 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
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
