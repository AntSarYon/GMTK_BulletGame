using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateManager: MonoBehaviour
{
    public NpcBaseState currentState;
    public NpcBaseState idleState = new NpcIdleState();
    public NpcWalkingState walkingState = new NpcWalkingState();
    public NpcShootingState shootState = new NpcShootingState();
    public NpcDefeatState defeatState = new NpcDefeatState();

    public float speed;

    public int teamNumber;

    public bool canControl;

    [Header("WalkingState")]
    public float visionRange;

    [Header("AttackState")]
    public float attackRange = 1.0f;
    public int damage = 10;
    public float attackRate = 1.0f;

    [Header("ShootState")]
    public GameObject projectile;
    public float minimumDistance;
    public float timeBetweenShots;
    public float shootingSpeed;
    public float maxiumRangeShoot;

    private void Start()
    {
        currentState = walkingState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        if (canControl)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchState(idleState);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchState(walkingState);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchState(shootState);
            }
        }
        currentState.UpdateState(this);
    }

    public void SwitchState(NpcBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
