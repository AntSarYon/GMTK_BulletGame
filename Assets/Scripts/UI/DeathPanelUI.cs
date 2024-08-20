using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathPanelUI : MonoBehaviour
{

    private Animator mAnimator;

    [Header("Referencia a HealthController")]
    [SerializeField] private PlayerHealthController HealthController;

    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private TextMeshProUGUI txtCinemaPer;

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
        //Asignamos los Datos de puntuación obtenida...
        txtTime.text = GameManager.Instance.currentRecordTime;
        txtCinemaPer.text = $"{((int)GameManager.Instance.currentCinemaPercentage)}%";

        //Corremos la animacion de Revelar Pantalla de Muerte
        mAnimator.Play("Reveal");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AudioManager.instance.updateSfxVolume(0);
        AudioManager.instance.updateBGValume(0);

    }

    public void Replay()
    {
        //Volvemos a cargar la escena del juego
        SceneManager.LoadScene("Playground_Jaime");

        AudioManager.instance.updateSfxVolume(0.35f);
        AudioManager.instance.updateBGValume(0.4f);
    }
}
