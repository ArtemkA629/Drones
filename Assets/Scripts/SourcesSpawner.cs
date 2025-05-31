using UnityEngine;
using Zenject;

public class SourcesSpawner : MonoBehaviour
{
    [Inject] private SourcesPool _pool;

    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private Vector2 _spawnAreaSize = new Vector2(10f, 10f);
    [SerializeField] private float _spawnHeight = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnSingleSource), 0f, _spawnInterval);
    }

    private void SpawnSingleSource()
    {
        Source source = _pool.GetSource();
        source.transform.position = CalculateSpawnPosition();
    }

    private Vector3 CalculateSpawnPosition()
    {
        float randomX = Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2);
        float randomZ = Random.Range(-_spawnAreaSize.y / 2, _spawnAreaSize.y / 2);
        return transform.position + new Vector3(randomX, _spawnHeight, randomZ);
    }
}
