using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_HealthManager : MonoBehaviour
{
    private SpriteRenderer mSPRender;
    private NpcBlurController mPCBlurController;

    [Header("Slider de Vida")]
    [SerializeField] private Slider mHealthSlider;

    private bool IsReceivingDamage;

    //Vida
    public float Health = 100;

    [Header("Velocidad de Decremento de Salud")]
    [SerializeField] private float healthDecreaseSpeed;

    [SerializeField] Image EnemyHealthBarFill;

    //--------------------------------------------------------------

    void Awake()
    {
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
        //Si el flag de recibiendo daño esta activo...
        if (IsReceivingDamage)
        {
            //Hacemos la Barra más opaca (oscura)
            EnemyHealthBarFill.color = new Color(1, 0.47f, 0.28f, 1);

            //Reducimos la salud gradualmente
            Health -= Time.deltaTime * healthDecreaseSpeed;

            //Actualizamos el Valor del Slider
            mHealthSlider.value = 100 - Health;

            if (Health <= 0)
            {
                Debug.Log("GANASTE");
            }
            else
            {
                // Debug.Log($"Vida restante: {Health}");
            }
        }

        //Si no estamos recibiendo daño
        else
        {
            //Hacemos la Barra Clara de nuevo
            EnemyHealthBarFill.color = new Color(1, 1, 1, 1);
        }
        
    }

    //---------------------------------------------------------------
}
