using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


//----------------------------------------------------

public class Projectile : MonoBehaviour
{
    //Escala del Proyectil
    private float myScale;

    //Escala principal del Proyectil (sera de 0 a 2)
    [SerializeField] private int principalScale;

    [Range(0,0.2f)] [SerializeField] private float blurValue;

    [Header("Sprites de Proyectiles")]
    [SerializeField] private Sprite[] arrSprites = new Sprite[5];

    //Referencia al Sprite Renderer
    private SpriteRenderer mSpRender;

    //---------------------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia
        mSpRender = GetComponent<SpriteRenderer>();

        //Asignamos el BlurMaterial creando una nueva instancia del mismo
        mSpRender.material = new Material(mSpRender.material);
    }

    //---------------------------------------------------------------------------

    void OnEnable()
    {
        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensChangedProDelegate;

        //Asignamos un Sprite aleatorio
        mSpRender.sprite = arrSprites[UnityEngine.Random.Range(0, arrSprites.Length)];
        //Asignamos una Escala para la Proporción idónea
        principalScale = Random.Range(0, 3);

        //Iniciamos indicando una Escala de 2 (GRANDE)
        myScale = 2;

        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(myScale, myScale, myScale);

        //Si la escala del ScalesManager es distinta que la Principal asignada a este proyectil,
        if (ScalesManager.Instance.scale != principalScale)
        {
            //Calculamos el Valor de Blur con la Escala de Foco actual
            blurValue = Mathf.Abs(ScalesManager.Instance.scale - principalScale) / 10;

            //Hacemos que el Sprite se vea grande
            myScale = Mathf.Lerp(0.5f, 2.00f, (blurValue/0.2f));
        }
        //Si la escala del ScalesManager es distinta que la Principal asignada a este proyectil,
        else
        {
            //Hacemos que el objeto se vea nitido
            blurValue = 0;

            //Restauro la Escala original del Proyectil
            myScale = 0.5f;
        }
    }

    //---------------------------------------------------------------------------

    private void TurnOffProjectile()
    {
        //Reducimos su velocidad a 0
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        //Desactivamos el objeto
        gameObject.SetActive(false);
    }

    //---------------------------------------------------------------------------
    void OnDisable()
    {
        //Quitamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged -= LensChangedProDelegate;
    }

    //---------------------------------------------------------------------------

    private void LensChangedProDelegate(float LenChange, float newScale)
    {
        //Calculamos el Valor de Blur con la Escala de Foco actual
        blurValue = (Mathf.Abs(newScale - principalScale) / 10) *2;

        //Actualizamos el valor del Blur
        mSpRender.material.SetFloat("_BlurAmount", blurValue);

        //Hacemos que el Sprite se vea grande
        myScale = Mathf.Lerp(0.5f, 2.00f, (blurValue / 0.2f));

        //Actualizamos la Escala del Proyectil constantemente 
        transform.localScale = new Vector3(myScale, myScale, myScale);
    }

    //---------------------------------------------------------------------------

    void Update()
    {
        //Hacemos que el Proyectil siempre mire hacia el Player
        transform.LookAt(SimplePlayerController.Instance.transform);

        //Actualizamos la Escala del Proyectil constantemente 
        //transform.localScale = new Vector3(myScale, myScale, myScale);

        //Actualizamos el valor del Blur
        //mSpRender.material.SetFloat("BlurAmount", blurValue);
    }

    //---------------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        //Si hemos impactado al jugador
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("MainCamera"))
        {
            //Desactivamos el Proyectil tras 3 segundos
            Invoke(nameof(TurnOffProjectile), 1);
        }

        //Si hemos impactado el suelo
        else if (collision.transform.CompareTag("Floor") || collision.transform.CompareTag("Column"))
        {
            //Desactivamos el Proyectil 
            TurnOffProjectile();
        }
    }
}
