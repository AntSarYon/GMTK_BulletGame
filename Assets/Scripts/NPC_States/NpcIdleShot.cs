using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcIdleShot : NpcShootState
{
    public override void EnterState(NpcStateManager npcStateManager)
    {
        npcStateManager.shootManager.enabled = false;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {

    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }
}
