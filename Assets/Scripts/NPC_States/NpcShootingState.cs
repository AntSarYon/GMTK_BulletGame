using System;
using UnityEngine;

public class NpcShootingState : NpcBaseState
{
    private Transform target;
    private Transform transform;
    public float minimumDistance = 5;
    private float nextShotTime = 0;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
       
    }

    private void Shoot(NpcStateManager npcStateManager)
    {
        GameObject bullet = UnityEngine.Object.Instantiate(npcStateManager.projectile, transform.position, Quaternion.identity);
        Vector2 direction = (target.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * npcStateManager.shootingSpeed;
        //bullet.GetComponent<Projectile>().damage = npcStateManager.damage;
        //bullet.GetComponent<Projectile>().teamNumber = npcStateManager.teamNumber;
    }
}
