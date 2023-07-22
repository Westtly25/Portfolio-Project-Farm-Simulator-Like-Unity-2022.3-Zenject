using Assets.Scripts.Architecture.AssetsManagment;

namespace Assets.Scripts.Architecture.State_System
{
    public class LoadProgressState : State
    {
        private readonly IAssetProvider assetProvider;

        public LoadProgressState(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}