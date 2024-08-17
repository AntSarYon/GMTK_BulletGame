using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShot : NpcShootingState
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
