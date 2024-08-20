using System;
using UnityEngine;

public class NpcBlurController : MonoBehaviour
{
    //Referencia a componente de vida
    private NPC_HealthManager npcHealthManager;

    //Flag de Escala normal
    public bool hasNormalScale;

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
        npcHealthManager = GetComponent<NPC_HealthManager>();

        //Asignamos el BlurMaterial creando una nueva instancia del mismo
        mSpRender.material = new Material(mSpRender.material);

        //La escala incia en 1
        myScale = 1;

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

        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(myScale, myScale, myScale);
    }

    //-----------------------------------------------------------------------

    private void LensChangedDelegate(float lenChange, float newScale)
    {
        //Calculamos el Valor de Blur con la Escala de Foco actual
        blurValue = (Mathf.Abs(newScale - principalScale) / 10);
        if (blurValue < 0.005f) blurValue = 0;

        //Modificamos el valor de la Escala
        myScale = Mathf.Lerp(1f, 4.00f, (blurValue / 0.2f));
        if (myScale < 1.2f) myScale = 1.00f;

        //Controlamos el estado de Flag de "tamaño normal"
        //Si tenemos la escala normal (1), activamos el flag
        if (myScale == 1) hasNormalScale = true;
        else
        {
            //Hacemos que deje de recibir daño
            hasNormalScale = false;
            npcHealthManager.StopRecievingDamage();
        }


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

        //Hacemos  que el enemigo deje de recibir daño
        hasNormalScale = false;
        npcHealthManager.StopRecievingDamage();

        //Calculamos el Valor de Blur con la Escala de Foco actual
        blurValue = (Mathf.Abs(ScalesManager.Instance.scale - principalScale) / 10);

        //Hacemos que el Sprite se vea grande
        myScale = Mathf.Lerp(1f, 4.00f, (blurValue / 0.2f));

        //hara el ataque
       //  GetComponent<NpcStateManager>().OrbitAttack();

    }

    //-----------------------------------------------------------------------

    void Update()
    {
        //Actualizamos la Escala del Proyectil en base al valor del Enum
        transform.localScale = new Vector3(myScale, myScale, myScale);

        //Actualizamos el valor del Blur
        mSpRender.material.SetFloat("_BlurAmount", blurValue);

        //Si tenemos la escala normal (1), activamos el flag
        if (myScale == 1)
        {
            hasNormalScale = true;
        }
        //Caso contrario, desactivamos el Flag
        else hasNormalScale = false;

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
