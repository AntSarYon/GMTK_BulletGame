using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//----------------------------------------------------

public class Projectile : MonoBehaviour
{
    //Escala del Proyectil
    private ProjectileScale myScale;

    [SerializeField] private Mesh highlightMesh;
    [SerializeField] private Mesh standardMesh;

    //Referencia al Mesh Renderer
    private MeshRenderer mMeshRender; //Recuerda CAMBIAR ESTO cuando uses Sprites en lugar de 3D
    private MeshFilter mMeshFilter;

    //---------------------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia
        mMeshRender = GetComponent<MeshRenderer>();

        //Obtenemos la Mesh por defecto
        mMeshFilter = GetComponent<MeshFilter>();
        standardMesh = mMeshFilter.mesh;

        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensChangedProDelegate;
    }

    //---------------------------------------------------------------------------

    void OnEnable()
    {
        //Obtenemos un indice de Escala aleatorio
        int randomScaleIndex = Random.Range(1, 4);

        //Empleamos un Switch para graduar la escala del Proyectil
        switch (randomScaleIndex)
        {
            case 1:
                myScale = ProjectileScale.x1;
                transform.localScale = ScalesManager.Instance.GetScaleValue(ProjectileScale.x1);
                break;
            case 2:
                myScale = ProjectileScale.x2;
                transform.localScale = ScalesManager.Instance.GetScaleValue(ProjectileScale.x2);
                break;
            case 3:
                myScale = ProjectileScale.x3;
                transform.localScale = ScalesManager.Instance.GetScaleValue(ProjectileScale.x3);
                break;
            default:
                break;
        }

        //Dependiendo de si la escala del ScalesManager es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        if (ScalesManager.Instance.currentLensScale != myScale)
        {
            //Desactivamos el Render del objeto
            mMeshFilter.mesh = standardMesh;
        }
        else
        {
            //Activamos el Render del objeto
            mMeshFilter.mesh = highlightMesh;
        }
    }

    void OnDestroy()
    {
        //Quitamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged -= LensChangedProDelegate;
    }

    //---------------------------------------------------------------------------

    private void LensChangedProDelegate(ProjectileScale newLensScale)
    {
        //Dependiendo de si la nueva escala es la misma que la del proyectil,
        //activamos o desactivmaos su componente de Renderizado.
        if (newLensScale != myScale)
        {
            //Desactivamos el Render del objeto
            mMeshFilter.mesh = standardMesh;
        }
        else
        {
            //Activamos el Render del objeto
            mMeshFilter.mesh = highlightMesh;
        }
    }

    //---------------------------------------------------------------------------


    void Update()
    {
        //Movemos el PROYECTI HACIA EL JUGADOR
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Time.deltaTime * 1.5f); ;
    }
}
