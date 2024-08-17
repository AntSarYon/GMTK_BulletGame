using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NpcWalkingState : NpcBaseState
{
    public float radius = 5;
    public Vector3 lastLoadedPos;
    private Vector3 randomPosition;
    private bool hasNewRandomPosition = false;
    private float timeSinceLastUpdate;
    public float updateIntervalMin = 2;
    public float updateIntervalMax = 3;
    private float updateInterval;
    public float maxDistance = 10;


    public override void EnterState(NpcStateManager npcStateManager)
    {
        
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        
    }
}
