using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_MOVEMENT : MonoBehaviour
{
    public float speed = 2.0f;

    private float minX;
    private float maxX;
    private Vector3 initialPosition;
    private float targetX;
    private float elapsedTime;

    void Start()
    {
        initialPosition = transform.position;
        UpdateRandomRange();
        targetX = Random.Range(minX, maxX);
    }

    void Update()
    {
        // Mover el objeto hacia la posición objetivo en X
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), speed * Time.deltaTime);

        // Si el objeto ha llegado al objetivo, seleccionar un nuevo objetivo
        if (Mathf.Approximately(transform.position.x, targetX))
        {
            targetX = Random.Range(minX, maxX);
        }

        // Actualizar el rango cada 2 segundos
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 2.0f)
        {
            UpdateRandomRange();
            targetX = Random.Range(minX, maxX);
            elapsedTime = 0f;
        }
    }

    void UpdateRandomRange()
    {
        // Actualizar los valores de minX y maxX
        minX = initialPosition.x + Random.Range(-5.0f, 0.0f);
        maxX = initialPosition.x + Random.Range(0.0f, 5.0f);
    }
}
