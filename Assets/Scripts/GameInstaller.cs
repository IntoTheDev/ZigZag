using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UserInput>().AsSingle().NonLazy();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
    }
}
