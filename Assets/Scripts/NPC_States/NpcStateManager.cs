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
    public ShootManager shootManager;
    public GameObject target;

    private void Start()
    {
        currentState = walkingState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchState(idleState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchState(walkingState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchState(shootState);
        }
        currentState.UpdateState(this);
    }

    public void SwitchState(NpcBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
