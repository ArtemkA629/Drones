using UnityEngine;
using Zenject;

public class DronesCountController : MonoBehaviour
{
    [SerializeField] private DronesCountSlider _dronesCountSlider;
    [SerializeField] private Drone[] _drones1;
    [SerializeField] private Drone[] _drones2;
    [SerializeField] private Collider _dronesArea;
    [SerializeField] private DronesCountControllerConfig _config;

    [Inject]
    public void Init()
    {
        var dronesCountChanger1 = CreateDronesCountChanger(_drones1);
        var dronesCountChanger2 = CreateDronesCountChanger(_drones2);
        _dronesCountSlider.Init(new DronesCountChanger[] { dronesCountChanger1, dronesCountChanger2 });
    }

    private DronesCountChanger CreateDronesCountChanger(Drone[] fraction) =>
        new DronesCountChanger(this, fraction, _dronesArea, _config.DronesYPosition);
}
