using UnityEngine;

public class NpcSimpleShotState : NpcShootState
{
    float shootsDelay = 2f;
    float launchForce = 5f;
    ShootManager shootManager;

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
            ShootFromRandomOrigin(npcStateManager);

            //Retornamos el Timer a 0
            npcStateManager.shootManager.shootTimer = 0;
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {

    }

    private void ShootFromRandomOrigin(NpcStateManager npcStateManager)
    {
        //Obtenemos un Indice aleatorio del Array
        int index = Random.Range(0, shootManager.origins.Length);

        int size = shootManager.SetSize();

        //Disparamos desde el Origen, indicando la fuerza del proyectil
        shootManager.origins[index].Shoot(launchForce, size);
    }
}
