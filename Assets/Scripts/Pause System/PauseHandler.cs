using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Pause_System
{
    public class PauseHandler : IPauseHandler, IInitializable
    {
        private readonly List<IPauseListener> listeners = new(20);
        private bool isPaused = false;

        [Header("Injected Data")]
        private readonly DiContainer container;

        public bool IsPaused
        {
            get => isPaused;
            private set => isPaused = value;
        }

        [Inject]
        public PauseHandler(DiContainer container) =>
            this.container = container;

        public void Initialize()
        {
            IList<IPauseListener> contracts = container.ResolveAll<IPauseListener>();

            for (int i = 0; i < contracts.Count; i++)
            {
                Register(contracts[i]);
            }
        }

        public void Register(IPauseListener listener) =>
            listeners.Add(listener);

        public void UnRegister(IPauseListener listener) =>
            listeners.Remove(listener);

        public void CleanUp() =>
            listeners.Clear();

        public void SetPaused(bool value)
        {
            IsPaused = value;

            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Pause(value);
            }
        }
    }
}