using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthController : MonoBehaviour
{
    public int health = 5;

    public event UnityAction<int> OnHealthChange;
    public event UnityAction OnPlayerDead;

    //------------------------------------------------

    private void ReduceHealth()
    {
        //Reducimos la salud en 1
        health--;

        if (OnHealthChange != null)
        {
            //Disparamos Evento de cambio de vida, enviando la nueva salud
            OnHealthChange?.Invoke(health);
        }
        

        if (health == 0)
        {
            if (OnPlayerDead != null)
            {
                //Disparamos evento
                OnPlayerDead?.Invoke();
            }  
        }
    }

    //------------------------------------------------

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
