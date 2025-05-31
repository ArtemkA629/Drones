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

    private List<Drone> activeDrones = new List<Drone>();
    private int targetDroneCount = 0;
    private Coroutine adjustmentCoroutine;

    public void OnSliderChanged(float value)
    {
        targetDroneCount = Mathf.RoundToInt(value);

        if (adjustmentCoroutine != null) StopCoroutine(adjustmentCoroutine);
        adjustmentCoroutine = StartCoroutine(AdjustDroneCount());
    }

    private IEnumerator AdjustDroneCount()
    {
        while (activeDrones.Count != targetDroneCount)
        {
            if (activeDrones.Count < targetDroneCount)
                SpawnDrone();
            else if (activeDrones.Count > targetDroneCount)
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
        activeDrones.Add(drone);
    }

    private void RemoveDrone()
    {
        if (activeDrones.Count == 0) return;

        int lastIndex = activeDrones.Count - 1;
        Drone drone = activeDrones[lastIndex];
        Destroy(activeDrones[lastIndex].gameObject);
        activeDrones.RemoveAt(lastIndex);
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