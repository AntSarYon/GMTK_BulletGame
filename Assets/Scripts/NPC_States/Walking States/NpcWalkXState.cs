using UnityEngine;

public class NpcWalkXState : NpcWalkState
{
    private float minX = -7;
    private float maxX = 7;
    private float offSet = 2;
    private Vector3 initialPosition;
    private float targetX;
    private float elapsedTime;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        initialPosition = npcStateManager.transform.position;
        UpdateRandomRange();
        targetX = Random.Range(minX, maxX);
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Mover el objeto hacia la posición objetivo en X
        npcStateManager.transform.position = Vector3.MoveTowards(npcStateManager.transform.position, new Vector3(targetX, npcStateManager.transform.position.y, npcStateManager.transform.position.z), npcStateManager.movementSpeed * Time.deltaTime);

        // Si el objeto ha llegado al objetivo, seleccionar un nuevo objetivo
        if (Mathf.Approximately(npcStateManager.transform.position.x, targetX))
        {
            targetX = Random.Range(minX, maxX);
        }

        // Actualizar el rango cada 2 segundos
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 2.0f)
        {
            UpdateRandomRange();
            targetX = Random.Range(minX, maxX);
            elapsedTime = 0f;
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posición deseada.
        Vector3 newPosition = npcStateManager.gameObject.transform.position;
        newPosition.x = npcStateManager.initialPosition.x;

        // Asigna el nuevo vector a la posición del transform.
        npcStateManager.transform.position = newPosition;
    }

    void UpdateRandomRange()
    {
        // Actualizar los valores de minX y maxX
        minX = initialPosition.x + Random.Range(-7.0f, -offSet);
        maxX = initialPosition.x + Random.Range(offSet, 7.0f);
    }
}
