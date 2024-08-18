using UnityEngine;

public class NpcWalkXYZState : NpcWalkState
{
    private float minX = -7, minY = -7, minZ = -7;
    private float maxX = 7, maxY = 7, maxZ = 7;
    private float offSet = 2;
    private float offSetY = 2;
    private float offSetZ = 2;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float elapsedTime;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        initialPosition = npcStateManager.transform.position;
        UpdateRandomRange();
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Mover el objeto hacia la posición objetivo en X, Y y Z
        npcStateManager.transform.position = Vector3.MoveTowards(npcStateManager.transform.position, targetPosition, npcStateManager.movementSpeed * Time.deltaTime);

        // Si el objeto ha llegado al objetivo, seleccionar un nuevo objetivo
        if (npcStateManager.transform.position == targetPosition)
        {
            targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        }

        // Actualizar los rangos cada 2 segundos
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 2.0f)
        {
            UpdateRandomRange();
            targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
            elapsedTime = 0f;
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posición inicial.
        Vector3 newPosition = npcStateManager.initialPosition;

        // Asigna el nuevo vector a la posición del transform.
        npcStateManager.transform.position = newPosition;
    }

    void UpdateRandomRange()
    {
        // Actualizar los valores de minX, minY, minZ, maxX, maxY, maxZ
        minX = initialPosition.x + Random.Range(-7.0f, -offSet);
        maxX = initialPosition.x + Random.Range(offSet, 7.0f);
        minY = initialPosition.y + Random.Range(-7.0f, -offSet) + offSetY;
        maxY = initialPosition.y + Random.Range(offSet, 7.0f) + offSetY;
        minZ = initialPosition.z + Random.Range(-7.0f, -offSet) + offSetZ;
        maxZ = initialPosition.z + Random.Range(offSet, 7.0f) + offSetZ;
    }
}
