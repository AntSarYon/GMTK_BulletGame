using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerColumnsController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1;
    
    //Flag de rotación
    [SerializeField] private bool mustRotate;

    //------------------------------------------------------

    void Awake()
    {
        //Iniciamos con el flag desactivado
        mustRotate = false;
    }

    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        //Si el flag de rotacionn esta activo...
        if (mustRotate)
        {
            transform.Rotate(0, rotationSpeed*Time.deltaTime, 0, Space.Self);
        }
    }
}
