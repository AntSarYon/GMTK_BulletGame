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

    [Header("Lens change AudioClips")]
    [SerializeField] private List<AudioClip> lensSoundsList = new List<AudioClip>();

    //Referencias a Componentes
    private MeshRenderer mMeshRender;
    private AudioSource mAudioSource;
    private Animator mAnimator;

    //Ultima escala asignada al Lente
    private ProjectileScale lastScale;

    //---------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia a componentes
        mMeshRender = GetComponentInChildren<MeshRenderer>();
        mAudioSource = GetComponent<AudioSource>();
        mAnimator = GetComponent<Animator>();
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
        //Actualizmaos la Ultima escala
        lastScale = newScale;

        //Reproducimos un sonido de cambio de lente
        mAudioSource.PlayOneShot(lensSoundsList[UnityEngine.Random.Range(0, lensSoundsList.Count)],1.00f);

        //Reproducimos Animación de Cambio de Lente
        mAnimator.Play("Change");
    }

    //---------------------------------------------------------------

    public void ChangeLensScaleMaterial()
    {
        //Dependiendo de cual sea la nueva escala, modificamos el Material del lens
        switch (lastScale)
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
