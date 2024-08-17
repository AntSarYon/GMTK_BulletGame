using System;
using UnityEngine;

public class NpcShootingState : NpcShootState
{
    public override void EnterState(NpcStateManager npcStateManager)
    {
        npcStateManager.shootManager.enabled = true;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
       
    }
}
