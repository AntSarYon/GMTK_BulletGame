using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FocusUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI txtFocusValue;
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
        //Actualizamos el texto
        txtFocusValue.text = $"x{Mathf.Round(scaleManager.scale * 100f) / 100f}'";

        //Actualizamos la Barra
        scrollBarFocus.value = ((scaleManager.scale * 100) / 2) / 100;
        Debug.Log(scrollBarFocus.value);
    }
}
