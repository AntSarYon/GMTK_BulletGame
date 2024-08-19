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
    public int minScale = 0;
    public int maxScale = 3;

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

    public int SetSize()
    {
        return UnityEngine.Random.Range(minScale, maxScale);
    }
}
