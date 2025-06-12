using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : IDroneMovement
{
    private readonly NavMeshAgent _agent;

    public NavMeshMovement(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public void MoveTo(Vector3 goal)
    {
        if (goal == Vector3.zero)
        {
            _agent.isStopped = true;
            return;
        }
        else
        {
            _agent.isStopped = false;
            _agent.destination = goal;
        }
    }
}
