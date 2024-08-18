using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasesLensController : MonoBehaviour
{
    
    private float minZoom = 50.00f;
    private float maxZoom = 70.00f;

    [SerializeField] private float multiplier = 10f;

    //---------------------------------------------------------------

    void Start()
    {
        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensScaleChangedDelegate;
    }

    //---------------------------------------------------------------

    private void LensScaleChangedDelegate(float lencChange)
    {
        Camera.main.fieldOfView = Mathf.Clamp(
            Camera.main.fieldOfView + (lencChange * multiplier), 
            minZoom, maxZoom
            );
    }
}
