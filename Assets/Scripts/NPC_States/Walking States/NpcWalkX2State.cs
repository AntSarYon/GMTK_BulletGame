using UnityEngine;

public class NpcWalkX2State : NpcWalkState
{
    public float range = 3f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        npcStateManager.transform.RotateAround(npcStateManager.target.transform.position, npcStateManager.speedDir * Vector3.up, npcStateManager.rotationSpeed * Time.deltaTime);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }
}
