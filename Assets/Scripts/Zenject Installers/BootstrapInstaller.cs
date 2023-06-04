using Zenject;
using Assets.Scripts.Player;
using Assets.Scripts.Input_System;

namespace Assets.Scripts.Zenject_Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputService>()
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
        }
    }
}