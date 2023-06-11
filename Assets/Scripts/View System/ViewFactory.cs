using System;
using Zenject;
using Assets.Scripts.Architecture.AssetsManagment;

namespace Assets.Scripts.View_System
{
    public class ViewFactory
    {
        private readonly DiContainer diContainer;
        private readonly AssetProvider assetProvider;

        [Inject]
        public ViewFactory(DiContainer diContainer, AssetProvider assetProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
        }

        public View CreateView()
        {


            return null;
        }
    }
}
