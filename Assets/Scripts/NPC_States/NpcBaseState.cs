using System;
using UnityEngine;

public abstract class NpcBaseState
{
    public abstract void EnterState(NpcStateManager npcStateManager);
    public abstract void UpdateState(NpcStateManager npcStateManager);
}
