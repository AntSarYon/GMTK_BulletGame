using UnityEngine;

public class NpcSimpleShotState : NpcShootState
{
    float shootsDelay = 2f;
    float launchForce = 15f;
    int bulletsCount = 0;
    ShootManager shootManager;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        if (npcStateManager.currentPhase == EnemyPhase.Phase1)
            shootsDelay = 2.35f;
        if (npcStateManager.currentPhase == EnemyPhase.Phase2)
            shootsDelay = 2f;
        if (npcStateManager.currentPhase == EnemyPhase.Phase3)
            shootsDelay = 1.65f;
        shootManager = npcStateManager.shootManager;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Incrementamos el Timer para los disparos
        shootManager.shootTimer += Time.deltaTime;

        // Si el Timer de disparo llega al limite
        if (shootManager.shootTimer >= shootsDelay)
        {
            // Disparamos de un Origen random
            ShootFromRandomOrigin(npcStateManager);
            bulletsCount++;

            // Retornamos el Timer de disparo a 0
            npcStateManager.shootManager.shootTimer = 0;
        }

        if (bulletsCount >= 5 && npcStateManager.currentPhase == EnemyPhase.Phase1)
        {
            // 50% de probabilidad de cambiar de estado
            if (Random.Range(0f, 1f) < 0.5f)
            {
                npcStateManager.SwitchWalkState(npcStateManager.idleWalk);
                npcStateManager.SwitchShootState(npcStateManager.burstShoot);
            }

            bulletsCount = 0;
        }
        if (bulletsCount >= 10 && npcStateManager.currentPhase == EnemyPhase.Phase2)
        {
            // 50% de probabilidad de cambiar de estado
            if (Random.Range(0f, 1f) < 0.8f)
            {
                npcStateManager.GetRandomWalkState(EnemyPhase.Phase2);
                npcStateManager.GetRandomShootState(EnemyPhase.Phase2);
            }

            bulletsCount = 0;
        }
        if (bulletsCount >= 5 && npcStateManager.currentPhase == EnemyPhase.Phase3)
        {
            // 50% de probabilidad de cambiar de estado
            if (Random.Range(0f, 1f) < 0.8f)
            {
                npcStateManager.GetRandomWalkState(EnemyPhase.Phase3);
                npcStateManager.GetRandomShootState(EnemyPhase.Phase3);
            }

            bulletsCount = 0;
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }

    private void ShootFromRandomOrigin(NpcStateManager npcStateManager)
    {
        // Obtenemos un Indice aleatorio del Array
        int index = Random.Range(0, shootManager.origins.Length);

        int size = shootManager.SetSize();

        // Disparamos desde el Origen, indicando la fuerza del proyectil
        shootManager.origins[index].Shoot(launchForce, size);
    }
}
