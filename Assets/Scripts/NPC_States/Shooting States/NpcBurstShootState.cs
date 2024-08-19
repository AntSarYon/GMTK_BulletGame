using UnityEngine;

public class NpcBurstShootState : NpcShootState
{
    float burstDelay = 2.5f; // Tiempo entre ráfagas
    float shootInterval = 0.1f; // Tiempo entre balas en una ráfaga
    int bulletsPerBurst = 10; // Número de balas por ráfaga
    float launchForce = 15f;
    ShootManager shootManager;
    int size;

    float shootTimer = 0f; // Temporizador para controlar las ráfagas
    bool isBursting = false; // Controla si está en una ráfaga

    public override void EnterState(NpcStateManager npcStateManager)
    {
        shootManager = npcStateManager.shootManager;
        shootTimer = 0f;
        isBursting = false;
        size = shootManager.SetSize();
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        shootTimer += Time.deltaTime;

        if (isBursting)
        {
            // Ejecutar la ráfaga
            if (shootTimer >= shootInterval)
            {
                ShootFromRandomOrigin(npcStateManager);
                shootTimer = 0f;
                bulletsPerBurst--;

                // Terminar la ráfaga después de disparar todas las balas
                if (bulletsPerBurst <= 0)
                {
                    isBursting = false;
                    bulletsPerBurst = 15; // Restablecer el conteo de balas para la próxima ráfaga
                }
            }
        }
        else
        {
            // Esperar el tiempo entre ráfagas
            if (shootTimer >= burstDelay)
            {
                isBursting = true;
                shootTimer = 0f;
                size = shootManager.SetSize();
            }
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Puedes añadir lógica para cuando termine el estado si es necesario
    }

    private void ShootFromRandomOrigin(NpcStateManager npcStateManager)
    {
        // Obtener un índice aleatorio del array
        int index = Random.Range(0, shootManager.origins.Length);

        // Disparar desde el origen, indicando la fuerza del proyectil
        shootManager.origins[index].Shoot(launchForce, size);
    }
}
