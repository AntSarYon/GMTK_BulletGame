using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthController : MonoBehaviour
{

    public bool PlayerIsAlive;

    public int health = 5;

    public event UnityAction<int> OnHealthChange;
    public event UnityAction OnPlayerDead;

    //Flag "Puede ser dañado"
    private bool canBeHurt;

    [Header("Tiempo de Invulnerabilidad")]
    [SerializeField] private float invulnerabilityTime = 1;
    private float currentInvulnerabilityTime;

    //------------------------------------------------

    void Awake()
    {
        //Flag de player vivo iniciado en True
        PlayerIsAlive = true;

        //Activamos Flag de "Puede ser dañado"
        canBeHurt = true;

        //Inicializamos tiempo actual de invulnerabilidad
        currentInvulnerabilityTime = 0;
    }

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

        //Si la salud del personaje esta en 0
        if (health == 0)
        {
            //Cambiamos Flag de "Jugador vivo" a falso
            PlayerIsAlive = false;

            if (OnPlayerDead != null)
            {
                //Disparamos evento de Muerte
                OnPlayerDead?.Invoke();
            }  
        }
    }

    //----------------------------------------------------

    void Update()
    {
        //Si el Flag de "Puede ser dañado" esta desactivado
        if (!canBeHurt)
        {
            //Incrementamos el Tiempo que llevamos en Invulnerabilidad
            currentInvulnerabilityTime += Time.deltaTime;

            //Si el tiempo de Invulnerabilidad actual llega al limite
            if (currentInvulnerabilityTime >= invulnerabilityTime)
            {
                //Activamos flag de "Puede ser dañado"
                canBeHurt =true;

                //Reiniciamos el tiempo actual de Invulnerabilidad
                currentInvulnerabilityTime = 0;
            }
        }
    }

    //-----------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        //Si hemos impactado con un Proyectil 
        if (collision.transform.CompareTag("Projectile"))
        {
            //Si el Player tiene el Flag de "Puede ser dañado" activo
            if (canBeHurt)
            {
                //Desactivamos Flag de "CanBeHurt"
                canBeHurt = false;

                //Reproducimos el quejido
                GetComponent<AudioSource>().Play();

                //Reducimos la Salud
                ReduceHealth();
            }
        }
    }
}
