using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealthController playerHealth;

    private Image healthBateryImage;

    [SerializeField] private Sprite Health0;
    [SerializeField] private Sprite Health1;
    [SerializeField] private Sprite Health2;
    [SerializeField] private Sprite Health3;
    [SerializeField] private Sprite Health4;
    [SerializeField] private Sprite Health5;

    //---------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia a componente de Imagen
        healthBateryImage = GetComponent<Image>();
    }

    //---------------------------------------------------------------

    void Start()
    {
        //Asignamos Funcion Delegada
        playerHealth.OnHealthChange += OnHealthChangeDelegate;

        //Iniciamos con el Sprite de Salud en 5
        healthBateryImage.sprite = Health5;
    }

    //---------------------------------------------------------------

    private void OnHealthChangeDelegate(int newHealth)
    {
        switch (playerHealth.health)
        {
            case 5:
                healthBateryImage.sprite = Health5;
                break;

            case 4:
                healthBateryImage.sprite = Health4;
                break;

            case 3:
                healthBateryImage.sprite = Health3;
                break;

            case 2:
                healthBateryImage.sprite = Health2;
                break;

            case 1:
                healthBateryImage.sprite = Health1;
                break;
            case 0:
                healthBateryImage.sprite = Health0;
                break;
            default:
                break;
        }
    }

    //---------------------------------------------------------------

    void OnDestroy()
    {
        //Quitamos la Funcion Delegada
        playerHealth.OnHealthChange -= OnHealthChangeDelegate;
    }

    //---------------------------------------------------------------
}
