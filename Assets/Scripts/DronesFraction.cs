using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DronesFraction : IEnumerable<Drone>
{
    [SerializeField] private Drone[] _drones;

    public IEnumerator<Drone> GetEnumerator()
    {
        foreach (var drone in _drones)
            yield return drone;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
