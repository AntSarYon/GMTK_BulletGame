using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcWalkXYZState : NpcWalkState
{
    private float startX;
    private float startY;
    private float startZ;
    public float range = 2f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        startX = npcStateManager.transform.position.x;
        startY = npcStateManager.transform.position.y;
        startZ = npcStateManager.transform.position.z;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        float newX = startX + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, range * 2) - range;
        float newY = startY + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, range * 2) - range;
        float newZ = startZ + Mathf.PingPong(Time.time * npcStateManager.movementSpeed, range * 2) - range;
        npcStateManager.transform.position = new Vector3(newX, newY, newZ);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posición deseada.
        Vector3 newPosition = npcStateManager.transform.position;
        newPosition.x = npcStateManager.initialPosition.x;
        newPosition.y = npcStateManager.initialPosition.y;
        newPosition.z = npcStateManager.initialPosition.z;

        // Asigna el nuevo vector a la posición del transform.
        npcStateManager.transform.position = newPosition;
    }
}
