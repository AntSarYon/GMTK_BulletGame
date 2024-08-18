using UnityEngine;

public class OrbitAroundTarget : MonoBehaviour
{
    public Transform target; // El objeto alrededor del cual orbitar� el cubo.
    public float speed = 10f; // Velocidad de la �rbita.

    void Update()
    {
        if (target != null)
        {
            // Calcular la rotaci�n alrededor del target en el eje Y autom�ticamente.
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
