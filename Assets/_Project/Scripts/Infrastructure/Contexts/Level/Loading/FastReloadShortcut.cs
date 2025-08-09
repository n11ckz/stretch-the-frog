using UnityEngine;
using Zenject;

namespace Project
{
    public class FastReloadShortcut : ITickable
    {
        private readonly LevelLoader _levelLoader;

        public FastReloadShortcut(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.R) == false)
                return;

            Execute();
        }

        public void Execute()
        {
            // TODO: condition for is menu showing;
            _levelLoader.ReloadCurrentLevel();
        }

    }
}
