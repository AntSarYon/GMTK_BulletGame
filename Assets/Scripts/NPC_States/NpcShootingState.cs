using System;
using UnityEngine;

public class NpcShootingState : NpcBaseState
{
    private Transform target;
    private Transform transform;
    public float minimumDistance = 5;
    private float nextShotTime = 0;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        npcStateManager.shootManager.enabled = true;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
       
    }
}
