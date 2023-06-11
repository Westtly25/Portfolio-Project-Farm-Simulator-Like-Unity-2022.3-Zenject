using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Pause_System
{
    public class PauseHandler : IPauseHandler, IInitializable
    {
        private readonly List<IPauseListener> listeners = new(20);

        [Header("Injected Data")]
        private readonly DiContainer container;

        public bool IsPaused { get; private set; } = false;

        [Inject]
        public PauseHandler(DiContainer container)
        {
            this.container = container;
        }

        public void Register(IPauseListener listener) =>
            listeners.Add(listener);

        public void UnRegister(IPauseListener listener) =>
            listeners.Remove(listener);

        public void CleanUp() =>
            listeners.Clear();

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;

            foreach (IPauseListener listener in listeners)
            {
                listener.Pause(isPaused);
            }
        }

        public void Initialize()
        {
            IEnumerable<IPauseListener> diListeners = (IEnumerable<IPauseListener>)container.GetDependencyContracts<IPauseListener>();

            foreach (var listener in diListeners)
            {
                Register(listener);
            }
        }
    }
}