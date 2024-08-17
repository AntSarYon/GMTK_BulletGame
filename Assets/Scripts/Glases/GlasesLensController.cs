using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasesLensController : MonoBehaviour
{
    public float opacityChangeSpeed = 0.1f; // Velocidad a la que cambia la opacidad

    public Renderer lenRenderer;
    private Color lenColor;

    //---------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia a componentes
        lenRenderer = GetComponentInChildren<Renderer>();
    }

    //---------------------------------------------------------------

    void Start()
    {
        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensScaleChangedDelegate;
    }

    //---------------------------------------------------------------

    private void LensScaleChangedDelegate(float scrollInput)
    {
        // Ajusta la opacidad del color del cubo
        lenColor.a = Mathf.Clamp(lenColor.a + scrollInput * opacityChangeSpeed, 0f, 1f);

        // Aplica el color modificado al material del cubo
        lenRenderer.material.color = lenColor;
    }
}
