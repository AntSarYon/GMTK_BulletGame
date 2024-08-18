using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform sujetadorDeCamara;

    //Definimos variables para controlar la rotaci�n m�xima y m�nima permitida
    public float MaxRot = 20f;
    public float MinRot = -20f;

    //---------------------------------------------------------------------------
    //Funci�n para Rotar la c�mara en Vertical (Recibe un Angulo Float)
    public void RotateUpDown(float angle)
    {
        //Definimos una nueva rotaci�n que ser� la misma, escepto por el Eje X...
        Vector3 newRotation = new Vector3(
            //Para el eje X, le a�adimos (o restamos) los angulos recibidos
            sujetadorDeCamara.rotation.eulerAngles.x + angle,
            sujetadorDeCamara.rotation.eulerAngles.y,
            sujetadorDeCamara.rotation.eulerAngles.z
        );

        //Para evitar BUGS de rotaci�n no deseados, controlamos el valor l�mites de los Angulos Euler
        newRotation.x = newRotation.x > 180f ? newRotation.x - 360f : newRotation.x;

        //Limitamos la rotaci�n entre el m�nimo y m�ximo permitido
        newRotation.x = Mathf.Clamp(newRotation.x, MinRot, MaxRot);

        //Actualizamos la rotaci�n del Transform de la c�mara
        sujetadorDeCamara.rotation = Quaternion.Euler(newRotation);

    }
}
