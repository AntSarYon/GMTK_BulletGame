using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FocusUI : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollBarFocus;

    private ScalesManager scaleManager;

    //----------------------------------------------

    void Start()
    {
        //Obtenemos referencia al Scales manager
        scaleManager = ScalesManager.Instance;
    }

    //----------------------------------------------

    void Update()
    {
        //Actualizamos la Barra
        scrollBarFocus.value = ((scaleManager.scale * 100) / 2) / 100;
    }
}
