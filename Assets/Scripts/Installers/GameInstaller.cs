using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField] private GameConfig _config = null;
    [SerializeField] private PathGenerator.Settings _generatorSettings = null;
    [SerializeField] private ColorChanger.Settings _colorChangerSettings = null;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UserInput>().AsSingle().NonLazy();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInstance(_config).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PathGenerator>().AsSingle().WithArguments(_generatorSettings).NonLazy();
        Container.BindInterfacesAndSelfTo<ColorChanger>().AsSingle().WithArguments(_colorChangerSettings).NonLazy();
    }
}