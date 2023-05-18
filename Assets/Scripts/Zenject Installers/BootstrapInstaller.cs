using Assets.Scripts.Input_System;
using Assets.Scripts.Player;
using Zenject;

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
        }
    }
}