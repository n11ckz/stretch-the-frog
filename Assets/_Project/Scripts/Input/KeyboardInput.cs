using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class KeyboardInput : IInput, ITickable
    {
        public event Action<Direction> DirectionReceived;

        private readonly PauseService _pauseService;

        public KeyboardInput(PauseService pauseService) =>
            _pauseService = pauseService;

        public void Tick()
        {
            if (_pauseService.IsPaused == true)
                return;

            ReadKeys();
        }

        private void ReadKeys()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) == true)
                DirectionReceived?.Invoke(Direction.Up);

            if (Input.GetKeyDown(KeyCode.DownArrow) == true)
                DirectionReceived?.Invoke(Direction.Down);

            if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
                DirectionReceived?.Invoke(Direction.Left);

            if (Input.GetKeyDown(KeyCode.RightArrow) == true)
                DirectionReceived?.Invoke(Direction.Right);
        }
    }
}
