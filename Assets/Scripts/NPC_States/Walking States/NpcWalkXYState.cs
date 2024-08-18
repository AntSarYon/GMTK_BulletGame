using UnityEngine;

public class NpcWalkXYState : NpcWalkState
{
    private float startY;
    public float range = 3f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        startY = npcStateManager.transform.position.y;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        float newY = startY + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, range * 2) - range;
        npcStateManager.transform.position = new Vector3(npcStateManager.transform.position.x, newY, npcStateManager.transform.position.z);
        npcStateManager.transform.RotateAround(npcStateManager.target.gameObject.transform.position, Vector3.up, npcStateManager.movementSpeed * Time.deltaTime);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posición deseada.
        Vector3 newPosition = npcStateManager.transform.position;
        newPosition.y = npcStateManager.initialPosition.y;

        // Asigna el nuevo vector a la posición del transform.
        npcStateManager.transform.position = newPosition;
    }
}
