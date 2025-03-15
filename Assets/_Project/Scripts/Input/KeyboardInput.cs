using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class KeyboardInput : IInput, ITickable
    {
        public event Action<Direction> DirectionReceived;

        private readonly PauseHandler _pauseHandler;

        public KeyboardInput(PauseHandler pauseHandler) =>
            _pauseHandler = pauseHandler;

        public void Tick()
        {
            if (_pauseHandler.IsPaused == true)
                return;
            
            ReadKeys();
        }

        private void ReadKeys()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
                DirectionReceived?.Invoke(Direction.Left);

            if (Input.GetKeyDown(KeyCode.RightArrow) == true)
                DirectionReceived?.Invoke(Direction.Right);

            if (Input.GetKeyDown(KeyCode.UpArrow) == true)
                DirectionReceived?.Invoke(Direction.Up);

            if (Input.GetKeyDown(KeyCode.DownArrow) == true)
                DirectionReceived?.Invoke(Direction.Down);
        }
    }
}
