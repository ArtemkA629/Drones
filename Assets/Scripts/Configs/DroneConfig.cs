using UnityEngine;

[CreateAssetMenu(fileName = "DroneConfig", menuName = "ScriptableObject/DroneConfig")]
public class DroneConfig : ScriptableObject
{
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField, Range(0f, 2f)] public float ReachDistance { get; private set; }
}
