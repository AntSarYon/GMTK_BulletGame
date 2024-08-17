using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    //Lista de Origenes de disparo
    public ProjectileOrigin[] origins;

    public GameObject projectilePrefab;
    public GameObject player;

    [Header("Tiempo entre disparos")]
    [Range(0.00f, 5.00f)] [SerializeField] private float shootsDelay;
    public float launchForce = 10f;

    //Timer de disparo
    private float shootTimer;

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

        Debug.Log($"Hay {origins.Length} orígenes de disparo.");
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
            LaunchProjectile();

            //Retornamos el Timer a 0
            shootTimer = 0;
        }
    }

    //--------------------------------------------------------------

    private void ShootFromRandomOrigin()
    {
        //Obtenemos un Indice aleatorio del Array
        int index = UnityEngine.Random.Range( 0, origins.Length );

        //Disparamos desde el Origen
        origins[index].Shoot();
    }

    void LaunchProjectile()
    {
        print("LaunchProjectile");
        // Crear una instancia del proyectil
        GameObject projectile = Instantiate(projectilePrefab, origins[0].transform.position, Quaternion.identity);

        // Calcular la dirección hacia el Player
        Vector3 directionToPlayer = (player.transform.position - origins[0].transform.position).normalized;

        // Agregar fuerza al proyectil en la dirección del Player
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(directionToPlayer * launchForce, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogError("El proyectil no tiene un componente Rigidbody.");
        }
    }
}
