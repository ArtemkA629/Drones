using System.Collections;
using System.Linq;
using UnityEngine;

public class DronesCountChanger
{
    readonly private MonoBehaviour _context;
    readonly private Drone[] _drones;
    readonly private Collider _area;
    readonly private float _ySpawnPosition;

    private int _targetDroneCount;
    private Coroutine _adjustmentCoroutine;

    public DronesCountChanger(MonoBehaviour context, Drone[] drones, Collider area, float ySpawnPosition)
    {
        _context = context;
        _drones = drones;
        _area = area;
        _ySpawnPosition = ySpawnPosition;
    }

    public void OnSliderChanged(float value)
    {
        _targetDroneCount = Mathf.RoundToInt(value);

        if (_adjustmentCoroutine != null) _context.StopCoroutine(_adjustmentCoroutine);
        _adjustmentCoroutine = _context.StartCoroutine(AdjustDroneCount());
    }

    private IEnumerator AdjustDroneCount()
    {
        var activeDrones = _drones.Where(x => x.gameObject.activeInHierarchy);

        while (activeDrones.Count() != _targetDroneCount)
        {
            if (activeDrones.Count() < _targetDroneCount)
                SpawnDrone();
            else if (activeDrones.Count() > _targetDroneCount)
                RemoveDrone();

            yield return null;
        }
    }

    private void SpawnDrone()
    {
        Vector3 spawnPosition = _area.GetRandomPointWithY(_ySpawnPosition);
        foreach (var drone in _drones)
        {
            if (drone.gameObject.activeInHierarchy)
                continue;

            drone.transform.position = spawnPosition;
            drone.gameObject.SetActive(true);
            break;
        }
    }

    private void RemoveDrone()
    {
        if (_drones.Length == 0) return;

        foreach (var drone in _drones)
        {
            if (!drone.gameObject.activeInHierarchy)
                continue;

            drone.gameObject.SetActive(false);
            break;
        }
    }
}