using UnityEngine;

public class NpcWalkXYState : NpcWalkState
{
    private float minY = -5;
    private float maxY = 5;
    private float offSet = 2;
    private float offSetY = 2;
    private Vector3 initialPosition;
    private float targetY;
    private float elapsedTime;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        initialPosition = npcStateManager.transform.position;
        UpdateRandomRange();
        targetY = Random.Range(minY, maxY) + offSetY;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Mover el objeto hacia la posición objetivo en Y
        npcStateManager.transform.position = Vector3.MoveTowards(npcStateManager.transform.position, new Vector3(npcStateManager.transform.position.x, targetY, npcStateManager.transform.position.z),npcStateManager.movementSpeed * Time.deltaTime);

        // Si el objeto ha llegado al objetivo, seleccionar un nuevo objetivo
        if (Mathf.Approximately(npcStateManager.transform.position.y, targetY))
        {
            targetY = Random.Range(minY, maxY) + offSetY;
        }

        // Actualizar el rango cada 2 segundos
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 2.0f)
        {
            UpdateRandomRange();
            targetY = Random.Range(minY, maxY);
            elapsedTime = 0f;
        }
        npcStateManager.transform.RotateAround(npcStateManager.target.gameObject.transform.position, Vector3.up, npcStateManager.speedDir * npcStateManager.rotationSpeed * Time.deltaTime);
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Crea un nuevo vector con la posición deseada.
        Vector3 newPosition = npcStateManager.gameObject.transform.position;
        newPosition.y = npcStateManager.initialPosition.y;

        // Asigna el nuevo vector a la posición del transform.
        npcStateManager.transform.position = newPosition;
    }

    void UpdateRandomRange()
    {
        // Actualizar los valores de minY y maxY
        minY = initialPosition.y + Random.Range(-7.0f, -offSet);
        maxY = initialPosition.y + Random.Range(offSet, 7.0f);
    }
}
