using System.Collections.Generic;
using UnityEngine.AI;

public class DronesSpeedController
{
    private List<Drone>[] _drones;

    public DronesSpeedController(List<Drone>[] drones)
    {
        _drones = drones;
    }

    public void OnSliderChanged(float speed)
    {
        foreach (var droneList in _drones)
            foreach (var drone in droneList)
                drone.GetComponent<NavMeshAgent>().speed = speed;
    }
}
