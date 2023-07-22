using Zenject;
using UnityEngine;
using Assets.Scripts.Pause_System;
using Assets.Scripts.Architecture.State_System;
using Assets.Scripts.Architecture.AssetsManagment;

namespace Assets.Scripts.Architecture
{
    public class App : MonoBehaviour
    {
        private StateMachine globalStateMachine = new ();

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
}