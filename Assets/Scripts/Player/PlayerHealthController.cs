using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int health = 3;

    //------------------------------------------------

    private void ReduceHealth()
    {
        //Reducimos la salud en 1
        health--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si hemos impactado con un Proyectil 
        if (collision.transform.CompareTag("Projectile"))
        {
            //Reproducimos el quejido
            GetComponent<AudioSource>().Play();

            //Reducimos la Salud
            ReduceHealth();

        }
    }
}
