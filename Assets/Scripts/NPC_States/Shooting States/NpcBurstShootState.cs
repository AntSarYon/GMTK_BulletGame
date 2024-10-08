using UnityEngine;

public class NpcBurstShootState : NpcShootState
{
    float burstDelay = 2.5f; // Tiempo entre r�fagas
    float shootInterval = 0.08f; // Tiempo entre balas en una r�faga
    int bulletsPerBurst = 5; // N�mero de balas por r�faga
    float launchForce = 15f;
    ShootManager shootManager;
    int size;

    float shootTimer = 0f; // Temporizador para controlar las r�fagas
    bool isBursting = false; // Controla si est� en una r�faga

    public override void EnterState(NpcStateManager npcStateManager)
    {
        shootManager = npcStateManager.shootManager;
        shootTimer = 0f;
        isBursting = false;
        size = shootManager.SetSize();
        
        AudioManager.instance.Play("BurstLineaWavesCuadrado");
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        shootTimer += Time.deltaTime;

        if (isBursting)
        {
            // Ejecutar la r�faga
            if (shootTimer >= shootInterval)
            {
                ShootFromRandomOrigin(npcStateManager);
                shootTimer = 0f;
                bulletsPerBurst--;

                // Terminar la r�faga despu�s de disparar todas las balas
                if (bulletsPerBurst <= 0)
                {
                    isBursting = false;
                    bulletsPerBurst = 15; // Restablecer el conteo de balas para la pr�xima r�faga

                    if (npcStateManager.currentPhase == EnemyPhase.Phase1 || npcStateManager.currentPhase == EnemyPhase.Phase2)
                    {
                        npcStateManager.GetRandomWalkState(npcStateManager.currentPhase);
                        npcStateManager.SwitchShootState(npcStateManager.simpleShoot);
                    }
                    if (npcStateManager.currentPhase == EnemyPhase.Phase3)
                    {
                        npcStateManager.GetRandomWalkState(EnemyPhase.Phase3);
                        npcStateManager.GetRandomShootState(EnemyPhase.Phase3);
                    }
                }
            }
        }
        else
        {
            // Esperar el tiempo entre r�fagas
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
        // Puedes a�adir l�gica para cuando termine el estado si es necesario
    }

    private void ShootFromRandomOrigin(NpcStateManager npcStateManager)
    {
        // Obtener un �ndice aleatorio del array
        int index = Random.Range(0, shootManager.origins.Length);

        // Disparar desde el origen, indicando la fuerza del proyectil
        shootManager.origins[index].Shoot(launchForce, size);
    }
}
