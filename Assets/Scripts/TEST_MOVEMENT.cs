using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_MOVEMENT : MonoBehaviour
{
    public float speed = 1f; // Velocidad del movimiento
    public float range = 3f; // Rango de movimiento en X

    private float startX; // Posici�n inicial en X

    void Start()
    {
        // Guardar la posici�n inicial en X
        startX = transform.position.x;
    }

    void Update()
    {
        // Calcular el nuevo X usando la posici�n inicial y el rango
        float newX = startX + Mathf.PingPong(Time.time * speed, range * 2) - range;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
