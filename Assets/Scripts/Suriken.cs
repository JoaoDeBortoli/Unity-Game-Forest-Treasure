using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suriken : MonoBehaviour
{
    public float speed;
    public Transform point1;
    public Transform point2;

    private Vector3 target;

    void Start()
    {
        target = point1.position;
    }

    void Update()
    {
        MoveSaw();
    }

    void MoveSaw()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Checa se a shuriken chegou no ponto de destino
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            // troca o target entre os 2 pontos
            target = (target == point1.position) ? point2.position : point1.position;
        }
    }
}
