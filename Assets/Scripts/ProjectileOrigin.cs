using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOrigin : MonoBehaviour
{
    public Transform parentTransform;

    void Start()
    {
        //Hacemos que el Origen siempre apunte hacia la cámara
        transform.LookAt(Camera.main.transform);
    }

    private void Update()
    {
        transform.rotation = parentTransform.rotation;
    }
    //-------------------------------------------------

    public void Shoot(float launchForce)
    {

        //Hacemos que el Origen siempre apunte hacia la cámara
        transform.LookAt(Camera.main.transform);

        //Solicitamos un Proyectil al Pool
        GameObject projectile = ObjectPooling.instance.AskForProjectile(transform.position);

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
        GameObject projectile = ObjectPooling.instance.AskForProjectile(transform.position + shootOffset);

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
