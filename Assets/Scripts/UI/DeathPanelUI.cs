using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanelUI : MonoBehaviour
{

    private Animator mAnimator;

    [Header("Referencia a HealthController")]
    [SerializeField] private PlayerHealthController HealthController;

    //-----------------------------------------------------

    void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    //-----------------------------------------------------

    void Start()
    {
        //Asignamos un Delegado al evento de morir
        HealthController.OnPlayerDead += OnPlayerDeadDelegate;
    }

    //-----------------------------------------------------

    private void OnPlayerDeadDelegate()
    {
        //Corremos la animacion de Revelar Pantalla de Muerte
        mAnimator.Play("Reveal");
    }
}
