using UnityEngine;
using Zenject;

public class SourcesSpawner : MonoBehaviour
{
    [SerializeField] private SourcesSpawnerConfig _config;

    private SourcesPool _pool;
    private float _spawnInterval;

    public float SpawnInterval
    {
        get => _spawnInterval;
        set
        {
            _spawnInterval = Mathf.Clamp(value, 0f, Mathf.Infinity);
            ChangeSpawnRate();
        }
    }

    [Inject]
    public void Init(SourcesPool pool)
    {
        _pool = pool;
        ChangeSpawnRate();
    }

    private void SpawnSingleSource()
    {
        Source source = _pool.GetSource();
        source.transform.position = CalculateSpawnPosition();
        source.Init();
    }

    private Vector3 CalculateSpawnPosition()
    {
        float randomX = Random.Range(-_config.SpawnAreaSize.x / 2, _config.SpawnAreaSize.x / 2);
        float randomZ = Random.Range(-_config.SpawnAreaSize.y / 2, _config.SpawnAreaSize.y / 2);
        return new Vector3(randomX, _config.SpawnHeight, randomZ);
    }

    private void ChangeSpawnRate()
    {
        CancelInvoke();

        float parabolaXDelta = _config.SpawnIntervalModifier;
        InvokeRepeating(nameof(SpawnSingleSource), _config.SpawnDelay, (_spawnInterval - parabolaXDelta) * (_spawnInterval - parabolaXDelta));
    }
}
