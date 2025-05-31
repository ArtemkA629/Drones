using UnityEngine;

public abstract class Storage : MonoBehaviour, IGoal
{
    public abstract void OnDroneReached();
}
