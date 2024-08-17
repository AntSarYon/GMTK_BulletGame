using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    private ProjectileScale currentLensScale;
    public ProjectileScale CurrentLensScale { get => currentLensScale; private set => currentLensScale = value; }

    //Diccionario de Escalas
    private Dictionary<ProjectileScale, Vector3> ScalesDic = new Dictionary<ProjectileScale, Vector3>();

    

    //-----------------------------------------------

    void Awake()
    {
        //Asignamos Instancia
        Instance = this;

        //Especificamos los Keys y valores del Diccionario de Escalas
        ScalesDic.Add(ProjectileScale.x1, new Vector3(0.15f, 0.15f, 0.15f));
        ScalesDic.Add(ProjectileScale.x2, new Vector3(0.45f, 0.45f, 0.45f));
        ScalesDic.Add(ProjectileScale.x3, new Vector3(0.8f, 0.8f, 0.8f));
    }

    //-------------------------------------------------

    public Vector3 GetScaleValue(ProjectileScale pScale)
    {
        //Retornamos el valor correspondiente a la Escala recibida
        return ScalesDic[pScale];

    }
}
