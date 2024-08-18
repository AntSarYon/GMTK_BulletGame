using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWalkXYState : NpcWalkState
{
    private float targetX;
    private float targetY;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        // Establece una posici�n objetivo inicial aleatoria en X y Y
        SetRandomTargetPosition(npcStateManager);
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Mueve el objeto hacia la posici�n objetivo en X e Y
        Vector3 currentPosition = npcStateManager.transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetX, npcStateManager.speed * Time.deltaTime);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, targetY, npcStateManager.speed * Time.deltaTime);
        npcStateManager.transform.position = currentPosition;

        // Si el objeto ha alcanzado la posici�n objetivo, establece una nueva
        if (Mathf.Approximately(currentPosition.x, targetX) && Mathf.Approximately(currentPosition.y, targetY))
        {
            SetRandomTargetPosition(npcStateManager);
        }
    }

    void SetRandomTargetPosition(NpcStateManager npcStateManager)
    {
        // Obtiene una nueva posici�n objetivo aleatoria en los rangos especificados
        float randomX = Random.Range(-3, 3);
        float randomY = Random.Range(-3, 3);

        // Calcula la nueva posici�n objetivo dentro de los l�mites permitidos
        targetX = Mathf.Clamp(npcStateManager.target.transform.position.x + randomX, npcStateManager.inferiorLimits.x, npcStateManager.superiorLimits.x);
        targetY = Mathf.Clamp(npcStateManager.target.transform.position.y + randomY, npcStateManager.inferiorLimits.y, npcStateManager.superiorLimits.y);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }
}
