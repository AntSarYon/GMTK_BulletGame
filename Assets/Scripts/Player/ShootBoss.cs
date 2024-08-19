using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBoss : MonoBehaviour
{
    [Header("Layer del Boss")]
    [SerializeField] private LayerMask BossLayer;

    //----------------------------------------------------------

    // Update is called once per frame
    void Update()
    {

        RaycastHit BossHit;
        bool wasBossHit = Physics.Raycast(transform.position, transform.forward, out BossHit, 50, BossLayer);
        if (wasBossHit)
        {
            print("BOSS IMPACTADO");
        }
    }

    //----------------------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 50);
    }
}
