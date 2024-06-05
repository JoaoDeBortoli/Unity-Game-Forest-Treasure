using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public GameObject arrowPrefab;    // Prefab da flecha
    public float fireRate;       // Taxa de disparo (em segundos)
    public AudioClip arrowSound;

    private float nextFireTime = 0f;

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            LaunchArrow();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void LaunchArrow()
    {
        // Instancia a flecha na posição e rotação do lançador
        Instantiate(arrowPrefab, transform.position, transform.rotation);
        SoundManager.sons.PlaySound(arrowSound);
    }
}
