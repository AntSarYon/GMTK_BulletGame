using System;
using UnityEngine;

public class NpcBlurController : MonoBehaviour
{
    //Escala del Proyectil
    private float myScale;

    //Escala principal del Proyectil (sera de 0 a 2)
    [SerializeField] private int principalScale;

    [Range(0, 0.2f)][SerializeField] private float blurValue;

    //Referencia al Sprite Renderer
    private SpriteRenderer mSpRender;

    private float BlurChangeTimer;

    [Header("Tiempo entre Cambios")]
    public float TimeBeforeNextBlurChange = 10;

    //-------------------------------------------------------

    private void Awake()
    {
        //Obtenemos referencia
        mSpRender = GetComponent<SpriteRenderer>();

        //Asignamos el BlurMaterial creando una nueva instancia del mismo
        mSpRender.material = new Material(mSpRender.material);

        BlurChangeTimer = 0;
    }

    //-----------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        
        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensChangedDelegate;
        
        //Asignamos una Escala principal aleatoria
        principalScale = UnityEngine.Random.Range(0, 3);

        myScale = 1;

        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(myScale, myScale, myScale);
    }

    //-----------------------------------------------------------------------

    private void LensChangedDelegate(float lenChange, float newScale)
    {
        //Calculamos el Valor de Blur con la Escala de Foco actual
        blurValue = (Mathf.Abs(newScale - principalScale) / 10);

        //Actualizamos el valor del Blur
        mSpRender.material.SetFloat("_BlurAmount", blurValue);

        //Hacemos que el Sprite se vea grande
        myScale = Mathf.Lerp(1f, 4.00f, (blurValue / 0.2f));

        //Actualizamos la Escala del Proyectil constantemente 
        transform.localScale = new Vector3(myScale, myScale, myScale);
    }

    //-----------------------------------------------------------------------

    public void ChangeBlurScale()
    {
        //Obtenemos una nueva escala
        int newScale = 0;
        do
        {
            //Asignaremos un aleatorio que no sea el mismo ya asignado
            newScale = UnityEngine.Random.Range(0, 3);
        } while (newScale == principalScale);

        //Asignamos la nueva escala como la Actual
        principalScale = newScale;
    }

    //-----------------------------------------------------------------------

    void Update()
    {
        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(myScale, myScale, myScale);

        //Incrementamos el Timer de BlurChange
        BlurChangeTimer += Time.deltaTime;

        //Si el Timer llega al Maximo antes del cambio...
        if (BlurChangeTimer >= TimeBeforeNextBlurChange)
        {
            //Cambiamos la Scala y el Blur
            ChangeBlurScale();

            //Devolvemos el Timer a 0
            BlurChangeTimer = 0;
        }
    }
}
