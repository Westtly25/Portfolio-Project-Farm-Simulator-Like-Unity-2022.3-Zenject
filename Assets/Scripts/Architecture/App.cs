using Zenject;
using UnityEngine;
using Assets.Scripts.Architecture.State_System;

namespace Assets.Scripts.Architecture
{
    public class App : MonoBehaviour
    {
        private StateMachine globalStateMachine = new StateMachine();

        public void Constructor()
        {

        }

        private void Awake() =>
            DontDestroyOnLoad(this);
    }
}