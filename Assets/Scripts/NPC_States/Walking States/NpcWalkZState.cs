using UnityEngine;

public class NpcWalkZState : NpcWalkState
{
    private float startZ;
    public float range = 2f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        startZ = npcStateManager.transform.position.z;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        float newZ = startZ + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, range * 2) - range;
        npcStateManager.transform.position = new Vector3(npcStateManager.transform.position.x, npcStateManager.transform.position.y, newZ);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posici�n deseada.
        Vector3 newPosition = npcStateManager.transform.position;
        newPosition.z = npcStateManager.initialPosition.z;

        // Asigna el nuevo vector a la posici�n del transform.
        npcStateManager.transform.position = newPosition;
    }
}
