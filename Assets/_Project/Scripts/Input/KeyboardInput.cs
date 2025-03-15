using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class KeyboardInput : IInput, ITickable
    {
        public event Action<Direction> OnDirectionReceived;

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
                OnDirectionReceived?.Invoke(Direction.Left);

            if (Input.GetKeyDown(KeyCode.RightArrow) == true)
                OnDirectionReceived?.Invoke(Direction.Right);

            if (Input.GetKeyDown(KeyCode.UpArrow) == true)
                OnDirectionReceived?.Invoke(Direction.Up);

            if (Input.GetKeyDown(KeyCode.DownArrow) == true)
                OnDirectionReceived?.Invoke(Direction.Down);
        }
    }
}
