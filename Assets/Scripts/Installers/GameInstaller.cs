using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField] private GameConfig _config = null;
    [SerializeField] private PathGenerator.Settings _generatorSettings = null;
    [SerializeField] private ColorChanger.Settings _colorChangerSettings = null;

    public override void InstallBindings()
    {
        InstallInput();
        InstallLogic();
    }

    private void InstallInput()
    {
        IUserInput input = null;

#if UNITY_EDITOR
        input = new EditorInput();
#elif UNITY_ANDROID || UNITY_IOS
        input = new MobileInput();
#endif
        
        Container.Bind(input.GetType().GetInterfaces()).FromInstance(input).AsSingle();
    }

    private void InstallLogic()
    {
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInstance(_config).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PathGenerator>().AsSingle().WithArguments(_generatorSettings).NonLazy();
        Container.BindInterfacesAndSelfTo<ColorChanger>().AsSingle().WithArguments(_colorChangerSettings).NonLazy();
    }
}