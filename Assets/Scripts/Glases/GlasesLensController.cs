using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasesLensController : MonoBehaviour
{
    [Header("Materiales de lentes")]
    [SerializeField] private Material mat_default;
    [SerializeField] private Material mat_ScaleX1;
    [SerializeField] private Material mat_ScaleX2;
    [SerializeField] private Material mat_ScaleX3;

    private MeshRenderer mMeshRender;

    //---------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia al MeshRender
        mMeshRender = GetComponent<MeshRenderer>();
    }

    //---------------------------------------------------------------

    void Start()
    {
        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensScaleChangedDelegate;
    }

    //---------------------------------------------------------------

    private void LensScaleChangedDelegate(ProjectileScale newScale)
    {
        //Dependiendo de cual sea la nueva escala, modificamos el Material del lens
        switch (newScale)
        {
            case ProjectileScale.x1:
                mMeshRender.material = mat_ScaleX1;
                break;
            case ProjectileScale.x2:
                mMeshRender.material = mat_ScaleX2;
                break;
            case ProjectileScale.x3:
                mMeshRender.material = mat_ScaleX3;
                break;
        }
    }

    //---------------------------------------------------------------

}
