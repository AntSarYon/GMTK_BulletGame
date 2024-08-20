using System;
using UnityEngine;

public enum EnemyPosition
{
    Near,
    Normal,
    Far
}

public enum EnemyPhase
{
    Phase1,
    Phase2,
    Phase3
}

public class NpcStateManager: MonoBehaviour
{
    // MOVEMENT STATES
    public NpcWalkState currentWalkingState;
    public NpcWalkState idleWalk = new NpcIdleState();
    public NpcWalkState walkingXState = new NpcWalkXState();
    public NpcWalkState walkingXYState = new NpcWalkXYState();
    public NpcWalkState walkingXYZState = new NpcWalkXYZState();
    public NpcWalkState walkingYZState = new NpcWalkYZState();
    public NpcWalkState walkingZState = new NpcWalkZState();
    public NpcWalkState walkingX2State = new NpcWalkX2State();
    public NpcWalkState simpleOrbit = new NpcSimpleOrbitX();
    public NpcWalkState fastOrbit = new NpcFastOrbitX();
    public NpcWalkState fakeTeleport = new NpcFakeTeleport();

    // SHOOT STATES
    public NpcShootState currentShootingState;
    public NpcShootState idleShoot = new NpcIdleShot();
    public NpcShootState simpleShoot = new NpcSimpleShotState();
    public NpcShootState burstShoot = new NpcBurstShootState();
    public NpcShootState boxShoot3x3 = new NpcBox9Shot();
    public NpcShootState lineShoot = new NpcLineShot();
    public NpcShootState boxShootMax = new NpcBox5x5Shot();

    public float movementSpeed;
    public float rotationSpeed;
    public int speedDir = 1;
    public ShootManager shootManager;
    public GameObject target;

    public Vector3 inferiorLimits;
    public Vector3 superiorLimits;
    public Vector3 initialPosition;

    public float minDistanceZ;
    public float maxDistanceZ;
    private float loadZ = 0;
    public Transform localPos;
    public int rangeTeleportZ = 75;

    public EnemyPosition currentPosition;
    public EnemyPhase currentPhase = EnemyPhase.Phase1;
    public bool _isDevMode;

    /*
     * Fase 1: speeds de 8, 15
     * Fase 2: speeds de 10, 20
     */


    private void Start()
    {
        initialPosition = transform.position;
        inferiorLimits += transform.position;
        superiorLimits += transform.position;
        currentWalkingState = idleWalk;
        currentShootingState = idleShoot;

        currentWalkingState.EnterState(this);
    }

    private void Update()
    {
        if (_isDevMode == true)
        {
            // idle
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(idleShoot);
            }
            // move horizontal
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWalkState(walkingXState);
                SwitchShootState(simpleShoot);
            }
            // orbitated on X
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWalkState(simpleOrbit);
                SwitchShootState(simpleShoot);
            }
            // orbitated on X and move vertical
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchWalkState(walkingXYState);
                SwitchShootState(idleShoot);
            }
            // move on X, Y and Z
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SwitchWalkState(fakeTeleport);
                SwitchShootState(idleShoot);
            }
            // move on Y and Z
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(boxShoot3x3);
            }
            //move on Z
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(boxShootMax);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(lineShoot);
            }
            currentWalkingState.UpdateState(this);
            currentShootingState.UpdateState(this);
        }
    }

    public void SwitchWalkState(NpcWalkState state)
    {
        currentWalkingState.EndState(this);RefreshLookTarget();
        NpcWalkState previousState = state;
        currentWalkingState = state;
        initialPosition = transform.position;
        if (currentWalkingState != fastOrbit || previousState != fastOrbit)
            GetRandomEnemyPosition();
        if (currentPhase != EnemyPhase.Phase1)
        {
            if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
            {
                speedDir = speedDir * -1;
            }
        }
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
        EnemyPosition newPosition;

        do
        {
            // Genera un número aleatorio entre 0 y el número de valores en el enum
            newPosition = (EnemyPosition)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyPosition)).Length);
        } while (newPosition == currentPosition);
        currentPosition = newPosition;

        switch (currentPosition)
        {
            case EnemyPosition.Near:
                transform.position = localPos.TransformPoint(new Vector3(0, 0, rangeTeleportZ + loadZ));
                loadZ = -rangeTeleportZ;
                break;
            case EnemyPosition.Normal:
                transform.position = localPos.TransformPoint(new Vector3(0, 0, 0 + loadZ));
                loadZ = 0;
                break;
            case EnemyPosition.Far:
                transform.position = localPos.TransformPoint(new Vector3(0, 0, -rangeTeleportZ + loadZ));
                loadZ = rangeTeleportZ;
                break;
            default:
                break;
            
        }

        initialPosition = transform.position;
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

    public void GetRandomWalkState(EnemyPhase phase)
    {
        if (phase == EnemyPhase.Phase1)
        {
            // Crear un array con todos los estados
            NpcWalkState[] walkStates = {
            idleWalk,
            walkingXState,
            walkingXYState,
            simpleOrbit,
        };

            // Seleccionar un índice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, walkStates.Length);

            // Retornar el estado correspondiente
            SwitchWalkState(walkStates[randomIndex]);
        }
        if (phase == EnemyPhase.Phase2)
        {
            // Crear un array con todos los estados
            NpcWalkState[] walkStates = {
            walkingXState,
            walkingXYState,
            walkingYZState,
            simpleOrbit,
            fastOrbit,
            fakeTeleport
        };

            // Seleccionar un índice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, walkStates.Length);

            // Retornar el estado correspondiente
            SwitchWalkState(walkStates[randomIndex]);
        }
        if (phase == EnemyPhase.Phase3)
        {
            // Crear un array con todos los estados
            NpcWalkState[] walkStates = {
            walkingXState,
            walkingXYState,
            walkingXYZState,
            walkingYZState,
            walkingZState,
            simpleOrbit,
            fastOrbit,
            fakeTeleport
        };

            // Seleccionar un índice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, walkStates.Length);

            // Retornar el estado correspondiente
            SwitchWalkState(walkStates[randomIndex]);
        }
    }

    public void GetRandomShootState(EnemyPhase phase)
    {
        if (phase == EnemyPhase.Phase1)
        {
            // Crear un array con todos los estados de disparo
            NpcShootState[] shootStates = {
            simpleShoot,
            simpleShoot,
            simpleShoot,
            simpleShoot,
            burstShoot,
        };

            // Seleccionar un índice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, shootStates.Length);

            // Retornar el estado correspondiente
            SwitchShootState(shootStates[randomIndex]);
        }
        if (phase == EnemyPhase.Phase2)
        {
            // Crear un array con todos los estados de disparo
            NpcShootState[] shootStates = {
            burstShoot,
            lineShoot,
            boxShoot3x3
        };

            // Seleccionar un índice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, shootStates.Length);

            // Retornar el estado correspondiente
            SwitchShootState(shootStates[randomIndex]);
        }
        if (phase == EnemyPhase.Phase3)
        {
            // Crear un array con todos los estados de disparo
            NpcShootState[] shootStates = {
            simpleShoot,
            simpleShoot,
            burstShoot,
            lineShoot,
            boxShootMax,
        };

            // Seleccionar un índice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, shootStates.Length);

            // Retornar el estado correspondiente
            SwitchShootState(shootStates[randomIndex]);
        }
    }
}
