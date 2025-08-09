using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ScreenService : ITickable
    {
        public event Action ResolutionChanged;

        public bool IsLandscapeResolution => Screen.width >= Screen.height;

        private float _lastWidth;
        private float _lastHeight;

        public void Tick()
        {
            if (_lastWidth == Screen.width && _lastHeight == Screen.height)
                return;

            UpdateResolution();
        }

        private void UpdateResolution()
        {
            _lastWidth = Screen.width;
            _lastHeight = Screen.height;

            ResolutionChanged?.Invoke();
        }
    }
}
