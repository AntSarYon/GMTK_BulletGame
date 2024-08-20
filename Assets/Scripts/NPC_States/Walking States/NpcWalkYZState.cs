using UnityEngine;

public class NpcWalkYZState : NpcWalkState
{
    private float startY;
    private float offSetY = 2;
    private float startZ;
    public float rangeY = 2f;
    public float rangeZ = 1f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        startY = npcStateManager.transform.position.y;
        startZ = npcStateManager.transform.position.z;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        float newY = startY + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, rangeY * 2) - rangeY;
        float newZ = startZ + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, rangeZ * 2) - rangeZ;
        npcStateManager.transform.position = new Vector3(npcStateManager.transform.position.x, newY + offSetY, newZ);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posición deseada.
        Vector3 newPosition = npcStateManager.transform.position;
        newPosition.y = npcStateManager.initialPosition.y;
        newPosition.z = npcStateManager.initialPosition.z;

        // Asigna el nuevo vector a la posición del transform.
        npcStateManager.transform.position = newPosition;
    }
}
