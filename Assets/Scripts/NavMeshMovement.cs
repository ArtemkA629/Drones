using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : IDroneMovement
{
    private NavMeshAgent _agent;

    public NavMeshMovement(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public void MoveTo(Vector3 goal)
    {
        _agent.destination = goal;
    }
}
