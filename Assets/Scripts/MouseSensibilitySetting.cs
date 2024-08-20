using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensibilitySetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtSensibilityValue;

    void Start()
    {
        //Asignamos la Sensibilidad almacenada en el GameManager
        GetComponent<Slider>().value = GameManager.Instance.MouseSensivility;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMouseSensibility(float newSensibility)
    {
        //Actualizamos la Sensivilidad del Mouse
        GameManager.Instance.MouseSensivility = newSensibility;

        //Actualizmaos el Texto
        txtSensibilityValue.text = newSensibility.ToString();
    }
}
