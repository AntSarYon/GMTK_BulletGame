using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NpcWalkXState : NpcWalkState
{
    private float startX;
    public float range = 3f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        startX = npcStateManager.transform.position.x;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        float newX = startX + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, range * 2) - range;
        npcStateManager.transform.position = new Vector3(newX, npcStateManager.transform.position.y, npcStateManager.transform.position.z);
        Debug.Log("UpdateState: " + npcStateManager.initialPosition);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posición deseada.
        Debug.Log(npcStateManager.initialPosition);
        Vector3 newPosition = npcStateManager.gameObject.transform.position;
        newPosition.x = npcStateManager.initialPosition.x;
        newPosition.y = npcStateManager.initialPosition.y;
        newPosition.z = npcStateManager.initialPosition.z;

        // Asigna el nuevo vector a la posición del transform.
        npcStateManager.transform.position = newPosition;
    }
}
