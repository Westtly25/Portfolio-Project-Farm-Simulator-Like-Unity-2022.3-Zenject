using Zenject;
using Assets.Scripts.Player;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<HeroFactory>()
                 .FromNew()
                 .AsSingle()
                 .NonLazy();

        Container.BindInterfacesAndSelfTo<NPCFactory>()
                 .FromNew()
                 .AsSingle()
                 .NonLazy();

        Container.Bind<IInventoryManager>()
                 .FromNew()
                 .AsSingle()
                 .NonLazy();
    }
}