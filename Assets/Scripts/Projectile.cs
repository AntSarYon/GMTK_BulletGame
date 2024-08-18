using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


//----------------------------------------------------

public class Projectile : MonoBehaviour
{
    //Escala del Proyectil
    private int myScale;

    [Header("Sprites de Proyectiles")]
    [SerializeField] private Sprite[] arrSprites = new Sprite[5];

    [Header("Material de Blur")]
    [SerializeField] private Material blurMaterial;

    [Header("Material default")]
    [SerializeField] private Material defaultMaterial;

    //Referencia al Sprite Renderer
    private SpriteRenderer mSpRender;

    //---------------------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia
        mSpRender = GetComponent<SpriteRenderer>();

        //Asignamos el BlurMaterial por defecto
        mSpRender.material = blurMaterial;
    }

    //---------------------------------------------------------------------------

    void OnEnable()
    {
        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensChangedProDelegate;

        //Asignamos un Sprite aleatorio
        mSpRender.sprite = arrSprites[UnityEngine.Random.Range(0, arrSprites.Length)];

        //Obtenemos un indice de Escala aleatorio
        int randomScaleIndex = Random.Range(1, 2);
        myScale = randomScaleIndex;

        float defaultScale = myScale / 2.5f;

        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        //Dependiendo de si la escala del ScalesManager es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        if (ScalesManager.Instance.scale != myScale)
        {
            //Ponemos el Objeto con Blur
            mSpRender.material = blurMaterial;
            //Hacemos que el Sprite se vea grande
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else
        {
            //Hacemos que el objeto se vea nitido
            mSpRender.material = defaultMaterial;

            //Restauro la Escala original del Proyectil
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        //Disparamos el Proyectile
        Launch();
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

    private void LensChangedProDelegate(float newLensScale)
    {
        float defaultScale = myScale / 2.5f;

        //Dependiendo de si la nueva escala es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        if (Mathf.Abs(ScalesManager.Instance.scale - myScale) > 1f)
        {
            //Ponemos el Objeto con Blur
            mSpRender.material = blurMaterial;
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else
        {
            //Hacemos que el objeto se vea nitido
            mSpRender.material = defaultMaterial;

            //Restauro la Escala original del Proyectil
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        }
    }

    //---------------------------------------------------------------------------

    private void Launch()
    {
        // Calcular la direcci�n hacia el Player
        Vector3 playerPosition = FindObjectOfType<SimplePlayerController>().transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;

        // Agregar fuerza al proyectil en la direcci�n del Player
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) 
            rb.AddForce(directionToPlayer * 8.5f, ForceMode.VelocityChange);
        else 
            Debug.LogError("El proyectil no tiene un componente Rigidbody.");
        
    }

    //---------------------------------------------------------------------------

    void Update()
    {
        //Hacemos que el Proyectil siempre mire hacia el Player
        transform.LookAt(SimplePlayerController.Instance.transform);
    }
}
