using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_PhaseAnimController : MonoBehaviour
{

    //Referencia a HelathManager
    private NPC_HealthManager healthManager;
    private Animator mAnimator;

    private void Awake()
    {
        healthManager = GetComponent<NPC_HealthManager>();
        mAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    }
}
