using UnityEngine;

public class DronesCountMediator : MonoBehaviour
{
    [SerializeField] private DronesCountController[] _dronesCountControllers;
    [SerializeField] private DronesCountSlider _dronesCountSlider;

    public void Start()
    {
        _dronesCountSlider.Init(_dronesCountControllers);
    }
}
