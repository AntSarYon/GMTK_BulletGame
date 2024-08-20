using System;
using UnityEngine;

public class NpcBox5x5Shot : NpcShootState
{
    float shootsDelay = 3f;
    float launchForce = 12f;
    ShootManager shootManager;

    int rows = 8;
    int columns = 8;
    float offset = 5f; // Offset entre balas

    public override void EnterState(NpcStateManager npcStateManager)
    {
        shootManager = npcStateManager.shootManager;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        //Incrementamos el Timer
        shootManager.shootTimer += Time.deltaTime;

        //Si el Timer llega al limite
        if (shootManager.shootTimer >= shootsDelay)
        {
            //Disparamos de un Origen random
            ShootBox(npcStateManager);

            //Retornamos el Timer a 0
            npcStateManager.shootManager.shootTimer = 0;

            if (npcStateManager.currentPhase == EnemyPhase.Phase3)
            {
                npcStateManager.GetRandomWalkState(EnemyPhase.Phase3);
                npcStateManager.GetRandomShootState(EnemyPhase.Phase3);
            }
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }

    private void ShootBox(NpcStateManager npcStateManager)
    {
        // Obtener el origen para disparar
        Transform origin = shootManager.origins[0].transform; // Asumimos que solo hay un origen y tomamos el primer índice

        int size = shootManager.SetSize();

        // Disparar en una formación de cuadrado
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // Calcula el offset para cada bala en la formación
                Vector3 positionOffset = new Vector3((column - (columns - 1) / 2f) * offset, (row - (rows - 1) / 2f) * offset, 0);

                // Aplica el offset a la posición del origen
                Vector3 shootDirection = origin.forward; // Dirección del disparo
                Vector3 shootPosition = origin.position + positionOffset;

                // Dispara desde la posición calculada
                shootManager.origins[0].Shoot(launchForce, positionOffset, positionOffset, size);
            }
        }
    }
}
