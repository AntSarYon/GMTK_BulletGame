using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_PhaseAnimController : MonoBehaviour
{

    //Referencia a HelathManager
    private NPC_HealthManager healthManager;
    private Animator mAnimator;
    public NpcStateManager npcStateManager;

    //----------------------------------------------------------

    private void Awake()
    {
        healthManager = GetComponent<NPC_HealthManager>();
        mAnimator = GetComponent<Animator>();
    }

    //----------------------------------------------------------

    void Update()
    {
        //Si estamos recibiendo da�o
        if (healthManager.IsReceivingDamage)
        {
            if (healthManager.Health <= 0)
            {
                mAnimator.Play("Die");
            }

            else if (healthManager.Health < 35)
            {
                mAnimator.Play("Phase3_Damage");
                if (npcStateManager.currentPhase != EnemyPhase.Phase3)
                    npcStateManager.ChangePhase(EnemyPhase.Phase3);
            }
            else if (healthManager.Health < 70)
            {
                mAnimator.Play("Phase2_Damage");
                if(npcStateManager.currentPhase != EnemyPhase.Phase2)
                    npcStateManager.ChangePhase(EnemyPhase.Phase2);
            }
            else
            {
                mAnimator.Play("Phase1_Damage");
            }
        }

        //Si no estamos recibiendo da�o
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
