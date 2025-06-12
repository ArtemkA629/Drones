using UnityEngine;

[CreateAssetMenu(fileName = "SourcesSpawnerConfig", menuName = "ScriptableObject/SourcesSpawnerConfig")]
public class SourcesSpawnerConfig : ScriptableObject
{
    [field: SerializeField, Range(0f, Mathf.Infinity)] public float SpawnHeight { get; private set; }
    [field: SerializeField, Range(0.1f, Mathf.Infinity)] public float SpawnDelay { get; private set; }
    [field: SerializeField] public float SpawnIntervalModifier { get; private set; }
    [field: SerializeField] public Vector3 SpawnAreaSize { get; private set; }
}
