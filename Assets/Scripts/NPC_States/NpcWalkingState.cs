using UnityEngine;

public class NpcWalkingState : NpcBaseState
{
    private float targetX;


    public override void EnterState(NpcStateManager npcStateManager)
    {
        
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        Debug.Log("pain");
        // Mueve el objeto hacia la posición objetivo en X
        Vector3 currentPosition = npcStateManager.transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetX, npcStateManager.speed * Time.deltaTime);
        npcStateManager.transform.position = currentPosition;

        // Si el objeto ha alcanzado la posición objetivo, establece una nueva
        if (Mathf.Approximately(currentPosition.x, targetX))
        {
            SetRandomTargetPosition(npcStateManager);
        }
    }

    void SetRandomTargetPosition(NpcStateManager npcStateManager)
    {
        // Obtiene una nueva posición objetivo aleatoria en el rango especificado
        float randomX = Random.Range(-3, +3);
        targetX = npcStateManager.target.transform.position.x + randomX;
    }
}
