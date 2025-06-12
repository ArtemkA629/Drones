using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class DronesSpeedController : MonoBehaviour
{
    [SerializeField] private DronesFraction[] _dronesFractions;
    [SerializeField] private DronesSpeedSlider _slider;

    [Inject]
    public void Init()
    {
        _slider.Init(this);
    }

    public void OnSliderChanged(float speed)
    {
        foreach (var fraction in _dronesFractions)
            foreach (var drone in fraction)
                drone.GetComponent<NavMeshAgent>().speed = speed;
    }
}
