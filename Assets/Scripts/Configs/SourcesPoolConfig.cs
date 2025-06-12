using UnityEngine;

[CreateAssetMenu(fileName = "SourcesPoolConfig", menuName = "ScriptableObject/SourcesPoolConfig")]
public class SourcesPoolConfig : ScriptableObject
{
    [field: SerializeField] public Source SourcePrefab { get; private set; }
    [field: SerializeField, Range(10, 100)] public int DefaultCapacity { get; private set; }
    [field: SerializeField, Range(100, 1000)] public int MaxPoolSize { get; private set; }
}
