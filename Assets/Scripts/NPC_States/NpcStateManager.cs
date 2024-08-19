using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyPosition
{
    Near,
    Normal,
    Far
}


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
    public NpcWalkState fastOrbit = new NpcFastOrbitX();

    // SHOOT STATES
    public NpcShootState currentShootingState;
    public NpcShootState idleShoot = new NpcIdleShot();
    public NpcShootState simpleShoot = new NpcSimpleShotState();
    public NpcShootState burstShoot = new NpcBurstShootState();
    public NpcShootState boxShoot = new NpcBox9Shot();
    public NpcShootState lineShoot = new NpcLineShot();

    public float movementSpeed;
    public float rotationSpeed;
    public ShootManager shootManager;
    public GameObject target;

    public Vector3 inferiorLimits;
    public Vector3 superiorLimits;
    public Vector3 initialPosition;

    public float minDistanceZ;
    public float maxDistanceZ;
    private float originalZ;

    public EnemyPosition currentEnemyPosition;

    private void Start()
    {
        initialPosition = transform.position;
        originalZ = initialPosition.z;
        inferiorLimits += transform.position;
        superiorLimits += transform.position;
        currentWalkingState = idleWalk;
        currentShootingState = idleShoot;
        SwitchWalkState(walkingX2State);
        SwitchShootState(boxShoot);

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
            SwitchShootState(lineShoot);
        }
        // orbitated on X
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWalkState(walkingX2State);
            SwitchShootState(boxShoot);
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
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SwitchWalkState(fastOrbit);
            SwitchShootState(idleShoot);
        }
        currentWalkingState.UpdateState(this);
        currentShootingState.UpdateState(this);
    }

    public void SwitchWalkState(NpcWalkState state)
    {
        currentWalkingState.EndState(this);
        GetRandomEnemyPosition();
        // RefreshLookTarget();
        currentWalkingState = state;
        initialPosition = transform.position;
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

    public void GetRandomEnemyPosition()
    {
        EnemyPosition newEnemyPosition;

        do
        {
            // Genera un número aleatorio entre 0 y el número de valores en el enum
            newEnemyPosition = (EnemyPosition)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyPosition)).Length);
        } while (newEnemyPosition == currentEnemyPosition);
        currentEnemyPosition = newEnemyPosition;

        PrintEnemyPosition(newEnemyPosition);
    }

    public void PrintEnemyPosition(EnemyPosition position)
    {
        switch (position)
        {
            case EnemyPosition.Near:
                transform.position = new Vector3(transform.position.x, transform.position.y, originalZ + 10);
                initialPosition = transform.position;
                break;
            case EnemyPosition.Normal:
                transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
                initialPosition = transform.position;
                break;
            case EnemyPosition.Far:
                transform.position = new Vector3(transform.position.x, transform.position.y, originalZ - 10);
                initialPosition = transform.position;
                break;
            default:
                break;
        }
    }

    void RefreshLookTarget()
    {
        // Obtén la dirección desde el enemigo hacia el jugador
        Vector3 direction = target.transform.position - transform.position;

        // Ajusta la dirección para que ignore el eje Y
        direction.y = 0;

        // Si la dirección es diferente de cero, ajusta la rotación
        if (direction != Vector3.zero)
        {
            // Calcula la rotación deseada
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Aplica la rotación instantáneamente
            transform.rotation = targetRotation;
        }
    }
}
