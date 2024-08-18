using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateManager: MonoBehaviour
{
    // MOVEMENT STATES
    public NpcWalkState currentWalkingState;
    public NpcWalkState idleWalk = new NpcIdleState();
    public NpcWalkState walkingXState = new NpcWalkXState();
    public NpcWalkState walkingX2State = new NpcWalkX2State();
    public NpcWalkState walkingXYState = new NpcWalkXYState();
    public NpcWalkState walkingXYZState = new NpcWalkXYZState();
    public NpcWalkState walkingYZState = new NpcWalkYZState();
    public NpcWalkState walkingZState = new NpcWalkZState();

    // SHOOT STATES
    public NpcShootState currentShootingState;
    public NpcShootState idleShoot = new NpcIdleShot();
    public NpcShootState simpleShoot = new NpcSimpleShotState();
    public NpcShootState burstShoot = new NpcBurstShootState();

    public float movementSpeed;
    public float rotationSpeed;
    public ShootManager shootManager;
    public GameObject target;

    public Vector3 inferiorLimits;
    public Vector3 superiorLimits;
    public Vector3 initialPosition;

    public float minDistanceZ;
    public float maxDistanceZ;

    private void Start()
    {
        initialPosition = transform.position;
        print("Start: " + initialPosition);
        inferiorLimits += transform.position;
        superiorLimits += transform.position;
        currentWalkingState = idleWalk;
        currentShootingState = idleShoot;

        currentWalkingState.EnterState(this);
    }

    private void Update()
    {
        // idle
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWalkState(idleWalk);
            SwitchShootState(idleShoot);
        }
        // move horizontal
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWalkState(walkingXState);
            SwitchShootState(idleShoot);
        }
        // orbitated on X
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWalkState(walkingX2State);
            SwitchShootState(simpleShoot);
        }
        // orbitated on X and move vertical
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWalkState(walkingXYState);
            SwitchShootState(simpleShoot);
        }
        // move on X, Y and Z
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWalkState(walkingXYZState);
            SwitchShootState(simpleShoot);
        }
        // move on Y and Z
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchWalkState(walkingYZState);
            SwitchShootState(simpleShoot);
        }
        //move on Z
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SwitchWalkState(walkingZState);
            SwitchShootState(simpleShoot);
        }
        currentWalkingState.UpdateState(this);
        currentShootingState.UpdateState(this);
    }

    public void SwitchWalkState(NpcWalkState state)
    {
        currentWalkingState.EndState(this);
        currentWalkingState = state;
        initialPosition = transform.position;
        print("SwitchWalkState transform: " + initialPosition);
        state.EnterState(this);
    }

    public void SwitchShootState(NpcShootState state)
    {
        currentShootingState.EndState(this);
        currentShootingState = state;
        state.EnterState(this);
    }

    private void OnDrawGizmos()
    {
        // Draw a line in the scene view to visualize the distance range
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z + minDistanceZ),
                        new Vector3(transform.position.x, transform.position.y, transform.position.z + maxDistanceZ));
    }
}
