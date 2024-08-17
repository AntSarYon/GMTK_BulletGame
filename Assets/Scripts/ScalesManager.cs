using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum ProjectileScale
{
    x1, x2, x3,
}
//-------------------------------

public class ScalesManager : MonoBehaviour
{
    //Variable de Instancia
    public static ScalesManager Instance;

    //Escala actual del juego
    [HideInInspector] public ProjectileScale currentLensScale;

    //Diccionario de Escalas
    private Dictionary<ProjectileScale, Vector3> ScalesDic = new Dictionary<ProjectileScale, Vector3>();

    [Header("Valores de Escalas")]
    [Range(0.00f, 1.00f)] [SerializeField] private float scaleX1;
    [Range(0.00f, 1.00f)] [SerializeField] private float scaleX2;
    [Range(0.00f, 1.00f)] [SerializeField] private float scaleX3;


    //Evento de Modificación de Escala de Lentes
    public event UnityAction<ProjectileScale> OnLensScaleChanged;

    //-----------------------------------------------

    void Awake()
    {
        //Inicizalizamos la escala de Lentes en x1
        currentLensScale = ProjectileScale.x1;

        //Asignamos Instancia
        Instance = this;
        
        //Especificamos los Keys y valores del Diccionario de Escalas
        ScalesDic.Add(ProjectileScale.x1, new Vector3(scaleX1, scaleX1, scaleX1));
        ScalesDic.Add(ProjectileScale.x2, new Vector3(scaleX2, scaleX2, scaleX2));
        ScalesDic.Add(ProjectileScale.x3, new Vector3(scaleX3, scaleX3, scaleX3));
    }

    //-------------------------------------------------

    public void LensScaleChange(ProjectileScale newLensScale)
    {
        //Llamamos a los delegados enviando la nueva escala de Lentes
        OnLensScaleChanged?.Invoke(newLensScale);
    }

    //-------------------------------------------------

    public Vector3 GetScaleValue(ProjectileScale pScale)
    {
        //Retornamos el valor correspondiente a la Escala recibida
        return ScalesDic[pScale];

    }
}
