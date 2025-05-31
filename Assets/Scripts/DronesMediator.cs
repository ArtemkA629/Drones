using System.Collections.Generic;
using UnityEngine;

public class DronesMediator : MonoBehaviour
{
    [SerializeField] private DronesCountController[] _dronesCountControllers;
    [SerializeField] private DronesCountSlider _dronesCountSlider;
    [SerializeField] private DronesSpeedSlider _dronesSpeedSlider;

    private DronesSpeedController _dronesSpeedController;

    public void Start()
    {
        _dronesCountSlider.Init(_dronesCountControllers);
        List<Drone>[] allDrones = new List<Drone>[_dronesCountControllers.Length];
        for (int i = 0; i < allDrones.Length; i++)
            allDrones[i] = _dronesCountControllers[i].ActiveDrones;
        _dronesSpeedController = new(allDrones);
        _dronesSpeedSlider.Init(_dronesSpeedController);
    }
}
