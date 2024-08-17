using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//----------------------------------------------------

public class Projectile : MonoBehaviour
{
    //Escala del Proyectil
    private ProjectileScale myScale;

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

        //Empleamos un Switch para graduar la escala del Proyectil
        switch (randomScaleIndex)
        {
            case 1:
                myScale = ProjectileScale.x1;
                break;
            case 2:
                myScale = ProjectileScale.x2;
                break;
            case 3:
                myScale = ProjectileScale.x3;
                break;
            default:
                break;
        }

        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = ScalesManager.Instance.GetScaleValue(myScale);

        //Dependiendo de si la escala del ScalesManager es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        if (ScalesManager.Instance.currentLensScale != myScale)
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
            transform.localScale = ScalesManager.Instance.GetScaleValue(myScale);
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

    private void OnLensScaleChangedDelegate(ProjectileScale newLensScale)
    {
        //Dependiendo de si la nueva escala es la misma que la del proyectil,
        //empleamos su Material normal, o el de BlurShader
        if (newLensScale != myScale)
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
            transform.localScale = ScalesManager.Instance.GetScaleValue(myScale);

        }
    }

    //---------------------------------------------------------------------------

    private void Launch()
    {
        // Calcular la dirección hacia el Player
        Vector3 playerPosition = FindObjectOfType<SimplePlayerController>().transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;

        // Agregar fuerza al proyectil en la dirección del Player
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
