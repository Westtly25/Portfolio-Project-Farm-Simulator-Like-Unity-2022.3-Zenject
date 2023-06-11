using Zenject;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Pause_System
{
    public class PauseHandler : IPauseHandler
    {
        public List<IPauseListener> listeners = new List<IPauseListener>(20);

        [Header("Injected Data")]
        private readonly DiContainer container;

        public bool IsPaused { get; private set; }

        [Inject]
        public PauseHandler(DiContainer container)
        {
            this.container = container;

            Initialize();
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

        private void Initialize()
        {
            IEnumerable<IPauseListener> diListeners = (IEnumerable<IPauseListener>)container.GetDependencyContracts<IPauseListener>();

            foreach (var listener in diListeners)
            {
                Register(listener);
            }

            listeners = diListeners.ToList();
        }
    }
}