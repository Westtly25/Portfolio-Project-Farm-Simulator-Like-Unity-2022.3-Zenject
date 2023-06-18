using UnityEngine;
using Assets.Scripts.Architecture.State_System;
using Assets.Scripts.Pause_System;
using Zenject;
using Assets.Scripts.Architecture.AssetsManagment;

namespace Assets.Scripts.Architecture
{
    public class App : MonoBehaviour
    {
        private StateMachine globalStateMachine = new StateMachine();

        [Inject]
        public void Constructor(IAssetProvider assetProvider,
                                IPauseHandler pauseHandler)
        {
            State[] states = new State[]
            {
                new LoadProgressState(assetProvider),
                new LoadSceneState(),
                new PauseState(pauseHandler)
            };
        }

        private void Awake() =>
            DontDestroyOnLoad(this);

        private void Update() =>
            globalStateMachine.Run();
    }

    public class LoadProgressState : State
    {
        public LoadProgressState(IAssetProvider assetProvider)
        {
        }
    }

    public class LoadSceneState : State
    {

    }

    public class PauseState : State
    {
        private readonly IPauseHandler pauseHandler;

        public PauseState(IPauseHandler pauseHandler)
        {
            this.pauseHandler = pauseHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            pauseHandler.SetPaused(true);
        }

        public override void OnExit()
        {
            base.OnExit();

            pauseHandler.SetPaused(false);
        }
    }
}