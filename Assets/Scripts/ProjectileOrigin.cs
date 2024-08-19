using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOrigin : MonoBehaviour
{
    public Transform fpChild;
    public GameObject temp;
    void Start()
    {
        //Hacemos que el Origen siempre apunte hacia la cámara
        transform.LookAt(Camera.main.transform);
    }

    //-------------------------------------------------

    public void Shoot(float launchForce)
    {

        //Hacemos que el Origen siempre apunte hacia la cámara
        transform.LookAt(Camera.main.transform);

        Quaternion quaternion = transform.rotation;

        //Solicitamos un Proyectil al Pool
        GameObject projectile = ObjectPooling.instance.AskForProjectile(fpChild.transform.position);

        // Calcular la direcci�n hacia el Player
        Vector3 playerPosition = Camera.main.transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;

        // Agregar fuerza al proyectil en la direcci�n del Player
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(directionToPlayer * launchForce, ForceMode.VelocityChange);
        else
            Debug.LogError("El proyectil no tiene un componente Rigidbody.");
    }

    public void Shoot(float launchForce, Vector3 shootOffset, Vector3 targetOffset)
    {
        // Hacemos que el Origen siempre apunte hacia la cámara
        transform.LookAt(Camera.main.transform);

        // Solicitar un Proyectil al Pool
        Vector3 firePosition = fpChild.TransformPoint(shootOffset);
        GameObject projectile = ObjectPooling.instance.AskForProjectile(firePosition);

        // Calcular la posición del objetivo con el offset
        Vector3 playerPosition = Camera.main.transform.position + targetOffset;

        // Calcular la dirección hacia el objetivo ajustado
        Vector3 directionToPlayer = (playerPosition - (transform.position + shootOffset)).normalized;

        // Agregar fuerza al proyectil en la dirección del objetivo
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(directionToPlayer * launchForce, ForceMode.VelocityChange);
        else
            Debug.LogError("El proyectil no tiene un componente Rigidbody.");
    }

}
