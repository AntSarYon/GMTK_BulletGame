using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBoss : MonoBehaviour
{
    [Header("Layer del Boss")]
    [SerializeField] private LayerMask BossLayer;

    //Referencia al Boss
    private GameObject Boss;

    //----------------------------------------------------------

    // Update is called once per frame
    void Update()
    {

        RaycastHit BossHit;
        bool wasBossHit = Physics.Raycast(transform.position, transform.forward, out BossHit, 80, BossLayer);
        
        if (wasBossHit)
        {
            Boss = BossHit.rigidbody.gameObject; 
            Boss.GetComponent<NPC_HealthManager>().StartRecievingDamage();
        }
        else
        {
            if (Boss != null)
            {
                Boss.GetComponent<NPC_HealthManager>().StopRecievingDamage();
            }
        }
    }

    //----------------------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 80);
    }
}
