using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//----------------------------------------------------

public class Projectile : MonoBehaviour
{
    //Escala del Proyectil
    private int myScale;

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

        //Obtenemos un indice de Escala aleatorio
        int randomScaleIndex = Random.Range(1, 2);
        myScale = randomScaleIndex;

        float defaultScale = myScale / 2.5f;

        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

        //Dependiendo de si la escala del ScalesManager es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        if (ScalesManager.Instance.scale != myScale)
        {
            //Ponemos el Objeto con Blur
            mSpRender.material = blurMaterial;
            //Hacemos que el Sprite se vea grande
            transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        }
        else
        {
            //Hacemos que el objeto se vea nitido
            mSpRender.material = defaultMaterial;

            //Restauro la Escala original del Proyectil
            transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
        }

        //Disparamos el Proyectile
        Launch();

        //Desactivamos tras 8 segundos
        Invoke(nameof(TurnOffProjectile), 12f);
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
            transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        }
        else
        {
            //Hacemos que el objeto se vea nitido
            mSpRender.material = defaultMaterial;

            //Restauro la Escala original del Proyectil
            transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

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
            rb.AddForce(directionToPlayer * 6, ForceMode.VelocityChange);
        else 
            Debug.LogError("El proyectil no tiene un componente Rigidbody.");
        
    }

    //---------------------------------------------------------------------------


    void Update()
    {
        //Movemos el PROYECTI HACIA EL JUGADOR
        //transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Time.deltaTime * 1.5f); ;
    }
}
