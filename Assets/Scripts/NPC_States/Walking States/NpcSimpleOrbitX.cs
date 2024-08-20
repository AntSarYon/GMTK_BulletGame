using UnityEngine;

public class NpcSimpleOrbitX : NpcWalkState
{
    public override void EnterState(NpcStateManager npcStateManager)
    {
        
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        npcStateManager.transform.RotateAround(npcStateManager.target.gameObject.transform.position, Vector3.up, npcStateManager.speedDir * npcStateManager.rotationSpeed * Time.deltaTime);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Puedes agregar cualquier lógica necesaria al finalizar el estado
    }
}
