using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBox : MonoBehaviour
{
    public AudioClip fireSound;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool triggered; //quando aciona a trap
    private bool active; //quando a trap esta ativada e pode dar dano

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(active)
        {
            gameObject.tag = "Enemy";
        }
        else
        {
            gameObject.tag = "Untagged";
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!triggered)
            {
                StartCoroutine(ActivateFirebox());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(active)
        {
            gameObject.tag = "Enemy";
        }
    }

    private IEnumerator ActivateFirebox()
    {
        // ativa a trap e da um feedbakc visual
        triggered = true;
        spriteRend.color = Color.red;

        // espera o tempo de ativação e ativa a trap
        yield return new WaitForSeconds(activationDelay);
        SoundManager.sons.PlaySound(fireSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        // desativa a trap e volta pro estado inicial
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }

}
