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

        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensChangedProDelegate;
    }

    //---------------------------------------------------------------------------

    void OnEnable()
    {
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
    }

    //---------------------------------------------------------------------------

    void OnDestroy()
    {
        //Quitamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged -= LensChangedProDelegate;
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


    void Update()
    {
        //Movemos el PROYECTI HACIA EL JUGADOR
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Time.deltaTime * 1.5f); ;
    }
}
