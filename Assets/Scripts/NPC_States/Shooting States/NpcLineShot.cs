using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class NpcLineShot : NpcShootState
{
    float shootsDelay = 1.5f;
    float launchForce = 7f;
    ShootManager shootManager;

    int columns = 10;
    float offset = 3.5f; // Offset entre balas

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
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }

    private void ShootBox(NpcStateManager npcStateManager)
    {
        // Obtener el origen para disparar
        Transform origin = shootManager.origins[0].transform; // Asumimos que solo hay un origen y tomamos el primer índice

        // Disparar en una formación de llínea
        for (int column = 0; column < columns; column++)
        {
            // Calcula el offset para cada bala en la formación
            Vector3 positionOffset = new Vector3((column - (columns - 1) / 2f) * offset, 0, 0);

            // Aplica el offset a la posición del origen
            Vector3 shootDirection = origin.forward; // Dirección del disparo
            Vector3 shootPosition = origin.position + positionOffset;

            int size = shootManager.SetSize();

            // Dispara desde la posición calculada
            shootManager.origins[0].Shoot(launchForce, positionOffset, positionOffset, size);
        }
    }
}
