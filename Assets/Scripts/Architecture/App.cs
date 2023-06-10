using Zenject;
using UnityEngine;
using Assets.Scripts.Architecture.State_System;
using UnityEditor.Localization.Plugins.Google.Columns;

namespace Assets.Scripts.Architecture
{
    public class App : MonoBehaviour
    {
        private StateMachine globalStateMachine = new StateMachine();

        private void Awake() =>
            DontDestroyOnLoad(this);

        private void Start() =>
            Initialize();

        private void Update()
        {
            globalStateMachine.Run();
        }

        private void Initialize()
        {
            State[] states = new State[]
            {
                new LoadProgressState(),
                new LoadSceneState()
            };
        }
    }

    public class LoadProgressState : State
    {

    }

    public class LoadSceneState : State
    {

    }
}