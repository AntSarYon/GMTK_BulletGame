using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//----------------------------------------------------

public class Projectile : MonoBehaviour
{
    //Escala del Proyectil
    private ProjectileScale myScale;

    //Referencia al Mesh Renderer
    private MeshRenderer mMeshRender; //Recuerda CAMBIAR ESTO cuando uses Sprites en lugar de 3D

    //---------------------------------------------------------------------------

    void Awake()
    {
        //Obtenemos referencia
        mMeshRender = GetComponent<MeshRenderer>();
    }

    //---------------------------------------------------------------------------

    void OnEnable()
    {
        //Asignamos funcion delegada
        ScalesManager.Instance.OnLensScaleChanged += LensChangedProDelegate;

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
            mMeshRender.enabled = false;
        }
        else
        {
            //Activamos el Render del objeto
            mMeshRender.enabled = true;
        }
    }

    void OnDisable()
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
            mMeshRender.enabled = false;
        }
        else
        {
            //Activamos el Render del objeto
            mMeshRender.enabled = true;
        }
    }

    //---------------------------------------------------------------------------


    void Update()
    {
        //Movemos el PROYECTI HACIA EL JUGADOR
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Time.deltaTime * 6.5f); ;
    }
}
