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

    //--------------------------------------------

    void Awake()
    {

        HUDTransform = GetComponent<RectTransform>();

        //Inicializamos escalas
        minScale = 0.85f;
        maxScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //HUDTransform.localScale = Vector3.Lerp()
    }
}
