using Assets.Scripts.Architecture.State_System;
using Assets.Scripts.Architecture.AssetsManagment;

namespace Assets.Scripts.Architecture
{
    public class LoadProgressState : State
    {
        private readonly IAssetProvider assetProvider;

        public LoadProgressState(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
    }
}