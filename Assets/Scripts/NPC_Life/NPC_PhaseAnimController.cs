using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_PhaseAnimController : MonoBehaviour
{

    //Referencia a HelathManager
    private NPC_HealthManager healthManager;
    private Animator mAnimator;

    //----------------------------------------------------------

    private void Awake()
    {
        healthManager = GetComponent<NPC_HealthManager>();
        mAnimator = GetComponent<Animator>();
    }

    //----------------------------------------------------------

    void Update()
    {
        //Si estamos recibiendo daño
        if (healthManager.IsReceivingDamage)
        {
            if (healthManager.Health <= 0)
            {
                mAnimator.Play("Die");
            }

            else if (healthManager.Health < 35)
            {
                mAnimator.Play("Phase3_Damage");
            }
            else if (healthManager.Health < 70)
            {
                mAnimator.Play("Phase2_Damage");
            }
            else
            {
                mAnimator.Play("Phase1_Damage");
            }
        }

        //Si no estamos recibiendo daño
        else
        {
            if (healthManager.Health <= 0)
            {
                mAnimator.Play("Die");
            }

            else if (healthManager.Health < 35)
            {
                mAnimator.Play("Phase3");
            }
            else if (healthManager.Health < 70)
            {
                mAnimator.Play("Phase2");
            }
            else
            {
                mAnimator.Play("Phase1");
            }
        }

    }
}
