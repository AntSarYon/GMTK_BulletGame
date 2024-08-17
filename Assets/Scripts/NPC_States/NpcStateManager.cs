using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateManager: MonoBehaviour
{
    public NpcWalkState currentWalkingState;
    public NpcWalkState idleWalk = new NpcIdleState();
    public NpcWalkState walkingState = new NpcWalkingState();

    public NpcShootState currentShootingState;
    public NpcShootState idleShoot = new NpcIdleShot();
    public NpcShootState simpleShoot = new NpcShootingState();

    public NpcDefeatState defeatState = new NpcDefeatState();

    public float speed;
    public ShootManager shootManager;
    public GameObject target;

    private void Start()
    {
        currentWalkingState = walkingState;
        currentShootingState = idleShoot;

        currentWalkingState.EnterState(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWalkState(idleWalk);
            SwitchShootState(idleShoot);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWalkState(walkingState);
            SwitchShootState(idleShoot);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWalkState(walkingState);
            SwitchShootState(simpleShoot);
        }
        currentWalkingState.UpdateState(this);
        currentShootingState.UpdateState(this);
    }

    public void SwitchWalkState(NpcWalkState state)
    {
        currentWalkingState = state;
        state.EnterState(this);
    }

    public void SwitchShootState(NpcShootState state)
    {
        currentShootingState = state;
        state.EnterState(this);
    }
}
