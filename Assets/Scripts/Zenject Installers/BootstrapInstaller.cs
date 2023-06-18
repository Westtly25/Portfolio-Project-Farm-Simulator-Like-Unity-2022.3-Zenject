using Zenject;
using Assets.Scripts.Player;
using Assets.Scripts.Input_System;
using Assets.Scripts.Pause_System;
using Assets.Scripts.Architecture.AssetsManagment;

namespace Assets.Scripts.Zenject_Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AssetProvider>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<InputService>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesAndSelfTo<PauseHandler>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesAndSelfTo<InventoryManager>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesAndSelfTo<AudioService>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<HeroFactory>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<NPCFactory>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();
        }
    }
}