using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _config = null;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UserInput>().AsSingle().NonLazy();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.BindInstance(_config);
    }
}
