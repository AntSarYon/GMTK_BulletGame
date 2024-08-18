using UnityEngine;

public class NpcBurstShootState : NpcShootState
{
    private float timer = 0f;
    private float interval = 3f;
    private bool isShooting = false;
    private float disableTimer = 1f;
    private float originalShootsDelay = 1f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        originalShootsDelay = npcStateManager.shootManager.shootsDelay;
        npcStateManager.shootManager.shootTimer = npcStateManager.shootManager.shootsDelay;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Reducir el temporizador en función del tiempo transcurrido
        timer -= Time.deltaTime;

        if (timer <= 0f && !isShooting)
        {
            // Ejecutar las acciones iniciales
            npcStateManager.shootManager.enabled = true;


            // Iniciar el timer para deshabilitar después de 1 segundo
            npcStateManager.shootManager.shootsDelay = 0.1f;
            isShooting = true;
            disableTimer = 1f;

            // Restablece el temporizador para el próximo intervalo
            timer = interval;
        }

        // Si estamos en el estado de disparo, reducir el temporizador de desactivación
        if (isShooting)
        {
            disableTimer -= Time.deltaTime;

            if (disableTimer <= 0f)
            {
                // Deshabilitar el shootManager después de 1 segundo
                npcStateManager.shootManager.enabled = false;

                // Resetea el estado de disparo
                isShooting = false;
            }
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        Debug.Log("set shoot");
        npcStateManager.shootManager.shootsDelay = originalShootsDelay;
    }
}
