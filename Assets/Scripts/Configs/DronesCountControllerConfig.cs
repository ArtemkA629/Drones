using UnityEngine;

[CreateAssetMenu(fileName = "DronesCountControllerConfig", menuName = "ScriptableObject/DronesCountControllerConfig")]
public class DronesCountControllerConfig : ScriptableObject
{
    [field: SerializeField] public float DronesYPosition { get; private set; }
}
