using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOrigin : MonoBehaviour
{
    void Start()
    {
        //Hacemos que el Origen siempre apunte hacia la cámara
        transform.LookAt(Camera.main.transform);

        Debug.Log($"{gameObject.name} apuntando al player");
    }

    void Update()
    {
        //Hacemos que el Origen siempre apunte hacia la cámara
        transform.LookAt(Camera.main.transform);
    }

    //-------------------------------------------------

    public void Shoot()
    {
        //Solicitamos un Proyectil al Pool
        SoapPool.instance.AskForProjectile(transform.position);
    }

}
