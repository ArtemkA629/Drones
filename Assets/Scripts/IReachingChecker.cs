using System;
using System.Collections;
using UnityEngine;

public interface IReachingChecker
{
    public event Action<GameObject> GoalReached;

    public IEnumerator CheckDistance(GameObject goal);
}
