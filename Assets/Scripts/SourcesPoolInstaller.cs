using UnityEngine;
using Zenject;

public class SourcesPoolInstaller : MonoInstaller
{
    [SerializeField] private SourcesPool _sourcePool;

    public override void InstallBindings()
    {
        Container.Bind<SourcesPool>().FromInstance(_sourcePool).AsSingle();
    }
}
