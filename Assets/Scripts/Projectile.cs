using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//----------------------------------------------------

public class Projectile : MonoBehaviour
{
    void OnEnable()
    {
        //Obtenemos un indice de Escala aleatorio
        int randomScaleIndex = Random.Range(1, 4);

        //Empleamos un Switch para graduar la escala del Proyectil
        switch (randomScaleIndex)
        {
            case 1:
                transform.localScale = ScalesManager.Instance.GetScaleValue(ProjectileScale.x1);
                break;
            case 2:
                transform.localScale = ScalesManager.Instance.GetScaleValue(ProjectileScale.x2);
                break;
            case 3:
                transform.localScale = ScalesManager.Instance.GetScaleValue(ProjectileScale.x3);
                break;
            default:
                break;
        }
    }

    void OnDisable()
    {
        
    }

    void Update()
    {
        //Movemos el PROYECTI HACIA EL JUGADOR
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Time.deltaTime * 6.5f); ;
    }
}
