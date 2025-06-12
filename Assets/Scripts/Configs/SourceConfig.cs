using UnityEngine;

[CreateAssetMenu(fileName = "SourceConfig", menuName = "ScriptableObject/SourceConfig")]
public class SourceConfig : ScriptableObject
{
    [field: SerializeField, Range(1f, 10f)] public float RotationDuration { get; private set; }
    [field: SerializeField, Range(0f, Mathf.Infinity)] public float DestroyDuration { get; private set; }
    [field: SerializeField, Range(0.1f, Mathf.Infinity)] public float ScaleDownFactor { get; private set; }
}

/**
RotationDuration = 3f;
DestroyDuration = 0.5f;
ScaleDownFactor = 0.3f;
**/