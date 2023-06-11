using System;
using System.Collections.Generic;

namespace Assets.Scripts.Pause_System
{
    public class PauseHandler : IPauseHandler
    {
        public List<IPauseListener> listeners = new List<IPauseListener>(20);

        public bool IsPaused { get; private set; }

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
    }
}