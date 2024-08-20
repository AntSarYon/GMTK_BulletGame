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

    private float timer = 0f;
    private float interval = 15f;

    private float originalY;

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
        originalY = transform.position.y;

        currentWalkingState.EnterState(this);
        currentShootingState.EnterState(this);
        Invoke(nameof(StartingStates), 3);
    }

    private void Update()
    {
        // Establece el intervalo seg�n la fase actual
        if (_isDevMode == false)
        {
            switch (currentPhase)
            {
                case EnemyPhase.Phase1:
                    interval = 20f;
                    break;
                case EnemyPhase.Phase2:
                    interval = 8f;
                    break;
                case EnemyPhase.Phase3:
                    interval = 15f;
                    break;
            }
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                SwitchWalkState(fakeTeleport);
                SwitchShootState(simpleShoot);
                timer = 0f;
            }
        }
        

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
                SwitchWalkState(idleWalk);
                SwitchShootState(simpleShoot);
            }
            // orbitated on X
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(burstShoot);
            }
            // orbitated on X and move vertical
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(boxShoot3x3);
            }
            // move on X, Y and Z
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(lineShoot);
            }
            // move on Y and Z
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchWalkState(idleWalk);
                SwitchShootState(boxShootMax);
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
        }
        currentWalkingState.UpdateState(this);
        currentShootingState.UpdateState(this);
    }

    public void SwitchWalkState(NpcWalkState state)
    {
        currentWalkingState.EndState(this);RefreshLookTarget();
        NpcWalkState previousState = state;
        currentWalkingState = state;
        initialPosition = transform.position;
        transform.position = new Vector3(initialPosition.x, originalY, initialPosition.z);
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
            // Genera un n�mero aleatorio entre 0 y el n�mero de valores en el enum
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
        // Obt�n la direcci�n desde el enemigo hacia el jugador
        Vector3 direction = target.transform.position - transform.position;

        // Ajusta la direcci�n para que ignore el eje Y
        direction.y = 0;

        // Si la direcci�n es diferente de cero, ajusta la rotaci�n
        if (direction != Vector3.zero)
        {
            // Calcula la rotaci�n deseada
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Aplica la rotaci�n instant�neamente
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

            // Seleccionar un �ndice aleatorio
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

            // Seleccionar un �ndice aleatorio
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

            // Seleccionar un �ndice aleatorio
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
            burstShoot,
        };

            // Seleccionar un �ndice aleatorio
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

            // Seleccionar un �ndice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, shootStates.Length);

            // Retornar el estado correspondiente
            SwitchShootState(shootStates[randomIndex]);
        }
        if (phase == EnemyPhase.Phase3)
        {
            // Crear un array con todos los estados de disparo
            NpcShootState[] shootStates = {
            simpleShoot,
            lineShoot,
            boxShootMax,
            burstShoot,
        };

            // Seleccionar un �ndice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, shootStates.Length);

            // Retornar el estado correspondiente
            SwitchShootState(shootStates[randomIndex]);
        }
    }

    public void ChangePhase(EnemyPhase phase)
    {
        currentPhase = phase;
        GetRandomWalkState(EnemyPhase.Phase3);
        GetRandomShootState(EnemyPhase.Phase3);
    }

    private void StartingStates()
    {
        SwitchWalkState(walkingXState);
        SwitchShootState(simpleShoot);
    }
}
