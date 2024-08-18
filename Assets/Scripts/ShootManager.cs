using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    //Lista de Origenes de disparo
    public ProjectileOrigin[] origins;

    [Header("Tiempo entre disparos")]
    [Range(0.00f, 5.00f)] [SerializeField] public float shootsDelay;
    public float launchForce = 10f;

    //Timer de disparo
    public float shootTimer;

    //----------------------------------------------------------------

    void Awake()
    {
        //Inicializmaos Timer
        shootTimer = 0;
    }

    //---------------------------------------------------------------------

    void Start()
    {
        //Obtenemos lista de origenes
        origins = GetComponentsInChildren<ProjectileOrigin>();

        // Debug.Log($"Hay {origins.Length} or�genes de disparo.");
    }

    //------------------------------------------------------------------

    void Update()
    {
        //Incrementamos el Timer
        shootTimer += Time.deltaTime;

        //Si el Timer llega al limite
        if (shootTimer >= shootsDelay)
        {
            //Disparamos de un Origen random
            ShootFromRandomOrigin();

            //Retornamos el Timer a 0
            shootTimer = 0;
        }
    }

    //--------------------------------------------------------------

    private void ShootFromRandomOrigin()
    {
        //Obtenemos un Indice aleatorio del Array
        int index = UnityEngine.Random.Range( 0, origins.Length );

        //Disparamos desde el Origen, indicando la fuerza del proyectil
        origins[index].Shoot(launchForce);
    }

    //--------------------------------------------------------------
}
