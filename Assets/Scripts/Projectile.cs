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
        ScalesManager.Instance.OnLensScaleChanged += OnLensScaleChangedDelegate;

        //Obtenemos un indice de Escala aleatorio
        int randomScaleIndex = Random.Range(1, 4);
        myScale = randomScaleIndex;

        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(myScale, myScale, myScale);

        //Dependiendo de si la escala del ScalesManager es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        if (ScalesManager.Instance.scale != myScale)
        {
            //Ponemos el Objeto con Blur
            mSpRender.material = blurMaterial;
            //Hacemos que el Sprite se vea grande
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else
        {
            //Hacemos que el objeto se vea nitido
            mSpRender.material = defaultMaterial;

            //Restauro la Escala original del Proyectil
            transform.localScale = new Vector3(myScale, myScale, myScale);
        }

        //Disparamos el Proyectile
        Launch();


    }

    //---------------------------------------------------------------------------
    void OnDisable()
    {
        //Quitamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged -= OnLensScaleChangedDelegate;
    }

    //---------------------------------------------------------------------------

    private void LensChangedProDelegate(float newLensScale)
    {
        //Dependiendo de si la nueva escala es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        print(ScalesManager.Instance.scale + "!=" + myScale);
        if (Mathf.Abs(ScalesManager.Instance.scale - myScale) > 1f)
        {
            //Ponemos el Objeto con Blur
            mSpRender.material = blurMaterial;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else
        {
            //Hacemos que el objeto se vea nitido
            mSpRender.material = defaultMaterial;

            //Restauro la Escala original del Proyectil
            transform.localScale = new Vector3(myScale, myScale, myScale);

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
            rb.AddForce(directionToPlayer * 10, ForceMode.VelocityChange);
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
