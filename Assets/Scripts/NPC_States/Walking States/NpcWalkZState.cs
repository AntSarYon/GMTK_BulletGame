using UnityEngine;

public class NpcWalkZState : NpcWalkState
{
    private float targetZ;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        SetRandomTargetPosition(npcStateManager);
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Mueve el objeto hacia la posici�n objetivo en Z
        Vector3 currentPosition = npcStateManager.transform.position;
        currentPosition.z = Mathf.MoveTowards(currentPosition.z, targetZ, npcStateManager.speed * Time.deltaTime);
        npcStateManager.transform.position = currentPosition;

        // Si el objeto ha alcanzado la posici�n objetivo, establece una nueva
        if (Mathf.Approximately(currentPosition.z, targetZ))
        {
            SetRandomTargetPosition(npcStateManager);
        }
    }

    void SetRandomTargetPosition(NpcStateManager npcStateManager)
    {
        // Obtiene una nueva posici�n objetivo aleatoria en el rango especificado
        float randomZ = Random.Range(-3, +3);
        targetZ = npcStateManager.target.transform.position.z + randomZ;
    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }
}
