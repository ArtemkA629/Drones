using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ClassicReachingChecker : IReachingChecker
{
    private static float _distanceToChangeGoal = 1f;

    private NavMeshAgent _agent;

    public ClassicReachingChecker(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public event Action<GameObject> GoalReached;

    public IEnumerator CheckDistance(GameObject goal)
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            if (_agent.remainingDistance < _distanceToChangeGoal)
            {
                GoalReached?.Invoke(goal);
                yield break;
            }
        }
    }
}
