using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingCamera : MonoBehaviour
{
    [Header("Referencia al HealthController")]
    [SerializeField] private PlayerHealthController healthController;

    private Animator mAnimator;

    //---------------------------------------------------------------

    void Awake()
    {
        //Referencia a componentes
        mAnimator = GetComponent<Animator>();
    }

    //---------------------------------------------------------------

    void Start()
    {
        //Asignamos Funcion delegado
        healthController.OnHealthChange += OnHealthChangeDelegate;
    }

    //------------------------------------------------------

    private void OnHealthChangeDelegate(int newHealth)
    {
        //Hacemos que la Cámara tiemble
        mAnimator.Play("ShakeDamage");
    }

}
