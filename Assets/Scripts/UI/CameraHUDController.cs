using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHUDController : MonoBehaviour
{
    //Imagen de HUD
    private RectTransform HUDTransform;

    //Escalas del HUD
    private float minScale;
    private float maxScale;

    [HideInInspector] public float interpolationValue;

    private float currentScale;

    //--------------------------------------------

    void Awake()
    {
        currentScale = 1;

        HUDTransform = GetComponent<RectTransform>();

        //Inicializamos escalas
        minScale = 0.85f;
        maxScale = 1.0f;
    }

    //--------------------------------------------

    // Update is called once per frame
    void Update()
    {
        //Actualizamos la escala empleando una inteprolacion
        currentScale = Mathf.Lerp(minScale, maxScale, interpolationValue);

        //Actualizamos constantemente el Scale del HUD 
        HUDTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}
