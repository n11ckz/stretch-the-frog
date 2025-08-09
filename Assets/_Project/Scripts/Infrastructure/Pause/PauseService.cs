using UnityEngine;

namespace Project
{
    public class PauseService
    {
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            if (IsPaused == true)
                return;

            IsPaused = true;
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            if (IsPaused == false)
                return;

            IsPaused = false;
            Time.timeScale = 1.0f;
        }
    }
}
