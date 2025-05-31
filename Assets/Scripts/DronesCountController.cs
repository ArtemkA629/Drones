using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DronesCountController : MonoBehaviour
{
    private static float _spawnInterval = 0.2f;
    private static float _ySpawnPosition = 4.2f;

    [Inject] private DiContainer _diContainer;

    [SerializeField] private Drone _dronePrefab;
    [SerializeField] private DroneInfo _droneInfo;
    [SerializeField] private Collider _spawnArea;

    private List<Drone> _activeDrones = new List<Drone>();
    private int _targetDroneCount = 0;
    private Coroutine _adjustmentCoroutine;

    public List<Drone> ActiveDrones => _activeDrones;

    public void OnSliderChanged(float value)
    {
        _targetDroneCount = Mathf.RoundToInt(value);

        if (_adjustmentCoroutine != null) StopCoroutine(_adjustmentCoroutine);
        _adjustmentCoroutine = StartCoroutine(AdjustDroneCount());
    }

    private IEnumerator AdjustDroneCount()
    {
        while (_activeDrones.Count != _targetDroneCount)
        {
            if (_activeDrones.Count < _targetDroneCount)
                SpawnDrone();
            else if (_activeDrones.Count > _targetDroneCount)
                RemoveDrone();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnDrone()
    {
        Vector3 spawnPosition = GetRandomPointInCollider();
        Drone drone = _diContainer.InstantiatePrefabForComponent<Drone>(_dronePrefab);
        drone.transform.position = spawnPosition;
        drone.Init(_droneInfo);
        _activeDrones.Add(drone);
    }

    private void RemoveDrone()
    {
        if (_activeDrones.Count == 0) return;

        int lastIndex = _activeDrones.Count - 1;
        Drone drone = _activeDrones[lastIndex];
        Destroy(_activeDrones[lastIndex].gameObject);
        _activeDrones.RemoveAt(lastIndex);
    }

    Vector3 GetRandomPointInCollider()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x),
            _ySpawnPosition,
            Random.Range(_spawnArea.bounds.min.z, _spawnArea.bounds.max.z)
        );

        return randomPoint;
    }
}