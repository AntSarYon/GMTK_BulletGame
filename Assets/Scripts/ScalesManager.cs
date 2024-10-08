using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScalesManager : MonoBehaviour
{
    //Evento Cambio de Escala de Lente
    public Action<float, float> OnLensScaleChanged;

    //Variable de Instancia
    public static ScalesManager Instance;

    //Escala inicial empieza en 1
    [HideInInspector] public float scale = 1;

    //Referencia al camara HUD
    [SerializeField] private CameraHUDController camHUD;

    //----------------------------------------------------

    void Awake()
    {
        //Asignamos Instancia
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //----------------------------------------------------

    public void LensScaleChanged(float scrollDir)
    {
        //Inicializamos valor de modificacion
        float lenChange = 0;

        //Dependiendo de si el scroll es positivo o negativo,
        //asignamos el valor de modificacion
        if (scrollDir > 0) lenChange = 0.2f;
        else if (scrollDir < 0) lenChange = -0.2f;

        //Modificamos la escala del Lente, limitando su posible valor, de 0 a 2
        scale = Mathf.Clamp(scale + lenChange, 0, 2);

        //Actualizamos el valor d eintyerpolacion para le HUD de la camara
        camHUD.interpolationValue = scale / 2;

        //Disparamos Evento enviando el valor de cambio
        OnLensScaleChanged?.Invoke(lenChange, scale);
    }
}
