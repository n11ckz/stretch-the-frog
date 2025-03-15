using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class PauseHandler
    {
        private readonly List<IPauseListener> _listeners = new List<IPauseListener>();

        public bool IsPaused { get; private set; }

        public void Pause()
        {
            if (IsPaused == true)
                return;
            
            IsPaused = true;
            Time.timeScale = 0.0f;

            foreach (IPauseListener listener in _listeners)
                listener.Pause();
        }

        public void Resume()
        {
            if (IsPaused == false)
                return;
            
            IsPaused = false;
            Time.timeScale = 1.0f;

            foreach (IPauseListener listener in _listeners)
                listener.Resume();
        }

        public void AddListener(IPauseListener listener) =>
            _listeners.Add(listener);

        public void RemoveListener(IPauseListener listener) =>
            _listeners.Remove(listener);
    }
}
