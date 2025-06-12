using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private SourcesPool _sourcesPool;
    [SerializeField] private SourcesSpawner _sourcesSpawner;
    [SerializeField] private DronesCountController _dronesCountController;
    [SerializeField] private DronesSpeedController _dronesSpeedController;
    [SerializeField] private Drone[] _drones;

    public override void InstallBindings()
    {
        BindSourcesPool();
        BindSourcesSpawner();
        BindDronesCountController();
        BindDronesSpeedController();
        BindDrones();
    }

    private void BindSourcesPool()
    {
        Container.Bind<SourcesPool>().FromInstance(_sourcesPool).AsSingle();
    }

    private void BindSourcesSpawner()
    {
        Container.Bind<SourcesSpawner>().FromInstance(_sourcesSpawner).AsSingle();
    }

    private void BindDronesCountController()
    {
        Container.Bind<DronesCountController>().FromInstance(_dronesCountController).AsSingle();
    }

    private void BindDronesSpeedController()
    {
        Container.Bind<DronesSpeedController>().FromInstance(_dronesSpeedController).AsSingle();
    }

    private void BindDrones()
    {
        foreach (var drone in _drones)
            Container.Bind<Drone>().FromInstance(drone);
    }
}
