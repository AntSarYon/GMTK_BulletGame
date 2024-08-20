using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPC_HealthManager : MonoBehaviour
{
    private SpriteRenderer mSPRender;
    private NpcBlurController mPCBlurController;

    [Header("Slider de Vida")]
    [SerializeField] private Slider mHealthSlider;

    public bool IsReceivingDamage;

    //Vida
    public float Health = 100;

    [Header("Velocidad de Decremento de Salud")]
    [SerializeField] private float healthDecreaseSpeed;

    [SerializeField] Image EnemyHealthBarFill;

    //Flag de Enemigo vivo
    private bool EnemyAlive;

    public event UnityAction OnBossDeath;

    //--------------------------------------------------------------

    void Awake()
    {
        //Iniciamos con el Flag de Enemigo vivo activo
        EnemyAlive = true;

        //Inicializamos Flags
        IsReceivingDamage = false;

        //Obtenemos referencias a componentes
        mSPRender = GetComponent<SpriteRenderer>();
        mPCBlurController = GetComponent<NpcBlurController>();


        //Inicalizamos la vida en 100
        Health = 100;
    }

    //--------------------------------------------------------------

    public void StartRecievingDamage()
    {
        //Si el Boss tiene el tamaño normal
        if (mPCBlurController.hasNormalScale)
        {
            //Asignamos color rojo
            mSPRender.color = new Color(1, 0.45f, 0.45f, 1);

            //Activamos Flag de Aplicando daño
            IsReceivingDamage = true;
        }
    }

    //--------------------------------------------------------------

    public void StopRecievingDamage()
    {
        //Asignamos color rojo
        mSPRender.color = new Color(1, 1, 1, 1);

        //Desactivamos Flag de "recibiendo daño"
        IsReceivingDamage = false;
    }

    void Update()
    {
        if (EnemyAlive)
        {
            //Si el flag de recibiendo daño esta activo...
            if (IsReceivingDamage)
            {
                //Hacemos la Barra más opaca (oscura)
                EnemyHealthBarFill.color = new Color(1, 0.47f, 0.28f, 1);

                //Reducimos la salud gradualmente
                Health -= Time.deltaTime * healthDecreaseSpeed;

                //Actualizamos el Valor del Slider -> Tmb en el GameManager
                mHealthSlider.value = 100 - Health;
                GameManager.Instance.currentCinemaPercentage = 100 - Health;

                //Si la vida ha llegado a 0
                if (Health <= 0)
                {
                    //Desactivamos el Flag
                    EnemyAlive = false;

                    //Desactivamos el Flag de GameManager para ya no tomar tiempo
                    GameManager.Instance.takeRecordTime = false;

                    //Disparamos el Evento de Boss muerto
                    OnBossDeath?.Invoke();
                }
            }

            //Si no estamos recibiendo daño
            else
            {
                //Hacemos la Barra Clara de nuevo
                EnemyHealthBarFill.color = new Color(1, 1, 1, 1);
            }
        }
        
        
    }

    //---------------------------------------------------------------
}
