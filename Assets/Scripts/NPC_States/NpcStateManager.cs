using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateManager: MonoBehaviour
{
    public NpcWalkState currentWalkingState;
    public NpcWalkState idleWalk = new NpcIdleState();
    public NpcWalkState walkingXState = new NpcWalkXState();
    public NpcWalkState walkingXYState = new NpcWalkXYState();
    public NpcWalkState walkingXYZState = new NpcWalkXYZState();

    public NpcShootState currentShootingState;
    public NpcShootState idleShoot = new NpcIdleShot();
    public NpcShootState simpleShoot = new NpcSimpleShotState();
    public NpcShootState burstShoot = new NpcBurstShootState();

    public NpcDefeatState defeatState = new NpcDefeatState();

    public float speed;
    public ShootManager shootManager;
    public GameObject target;

    public Vector3 inferiorLimits;
    public Vector3 superiorLimits;
    public Transform gizmosTransform;

    private void Start()
    {
        inferiorLimits += transform.position;
        superiorLimits += transform.position;
        currentWalkingState = walkingXState;
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
            SwitchWalkState(walkingXState);
            SwitchShootState(idleShoot);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWalkState(walkingXState);
            SwitchShootState(simpleShoot);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWalkState(walkingXYState);
            SwitchShootState(burstShoot);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWalkState(walkingXYZState);
            SwitchShootState(simpleShoot);
        }
        currentWalkingState.UpdateState(this);
        currentShootingState.UpdateState(this);
    }

    public void SwitchWalkState(NpcWalkState state)
    {
        currentWalkingState.EndState(this);
        currentWalkingState = state;
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
        // Cambia el color de los Gizmos
        Gizmos.color = Color.green;

        // Dibuja un cuadro que representa los límites de movimiento
        Gizmos.DrawWireCube(
            gizmosTransform.position,  // Centro del cuadro
            new Vector3(
                superiorLimits.x - inferiorLimits.x, 
                superiorLimits.y - inferiorLimits.y, 
                superiorLimits.z - inferiorLimits.z
               )               // Tamaño del cuadro
        );
    }
}
