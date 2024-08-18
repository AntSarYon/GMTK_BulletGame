using UnityEngine;

public class OrbitAroundTarget : MonoBehaviour
{
    public Transform target; // El objeto alrededor del cual orbitará el cubo.
    public float speed = 10f; // Velocidad de la órbita.

    void Update()
    {
        if (target != null)
        {
            // Calcular la rotación alrededor del target en el eje Y automáticamente.
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
