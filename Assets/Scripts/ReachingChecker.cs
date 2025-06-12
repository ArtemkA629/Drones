using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ReachingChecker
{
    private readonly NavMeshAgent _agent;
    private readonly float _reachDistance;

    public ReachingChecker(NavMeshAgent agent, float reachDistance)
    {
        _agent = agent;
        _reachDistance = reachDistance;
    }

    public event Action<Goal> GoalReached;

    public IEnumerator CheckDistance(Goal goal)
    {
        while (true) 
        {
            yield return new WaitForSeconds(Time.deltaTime);

            if (_agent.remainingDistance < _reachDistance)
            {
                GoalReached?.Invoke(goal);
                yield break;
            }
        }
    }
}
