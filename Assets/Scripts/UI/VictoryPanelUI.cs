using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPanelUI : MonoBehaviour
{
    private Animator mAnimator;

    [Header("Referencia a HealthController")]
    [SerializeField] private NPC_HealthManager BossHealthController;

    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private Image currentBateryLifeImg;
    [SerializeField] private Image FinalBateryLifeImg;

    void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        //Asignamos un Delegado al evento de morir
        BossHealthController.OnBossDeath += OnBossDeathDelegate;
    }

    private void OnBossDeathDelegate()
    {
        //Asignamos los Datos de puntuación obtenida...
        txtTime.text = GameManager.Instance.currentRecordTime;

        //Hacemos que el Sprite de Bateria sea el mismo que el ultimo
        FinalBateryLifeImg.sprite = currentBateryLifeImg.sprite;

        //Corremos la animacion de Revelar Pantalla de Muerte
        mAnimator.Play("Reveal");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //---------------------------------------------------------------

    public void Replay()
    {
        //Volvemos a cargar la escena del juego
        SceneManager.LoadScene("Playground_Jaime");
    }
}
