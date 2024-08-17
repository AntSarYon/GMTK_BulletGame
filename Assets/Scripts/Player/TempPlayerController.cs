using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ScalesManager.Instance.LensScaleChange(ProjectileScale.x1);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ScalesManager.Instance.LensScaleChange(ProjectileScale.x2);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ScalesManager.Instance.LensScaleChange(ProjectileScale.x3);
        }
    }
}
