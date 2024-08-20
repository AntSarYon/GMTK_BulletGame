using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerColumnsController : MonoBehaviour
{
    float rotationSpeed = 60;

    Vector3 currentEulerAngles;

    float y;


    //------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        y = -1;

        currentEulerAngles += new Vector3(0, y, 0) * Time.deltaTime * rotationSpeed;

        transform.localEulerAngles = currentEulerAngles;
    }
}
