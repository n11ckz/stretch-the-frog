using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class TouchscreenInput : IInput, ITickable
    {
        private const int PrimaryTouchIndex = 0;
        private const int SwipeThreshold = 125;

        public event Action<Direction> DirectionReceived;

        private readonly PauseHandler _pauseHandler;

        private bool HasAnyTouches => Input.touchCount > 0;

        private Vector2 _initialTouchPosition;
        private bool _isSwiping;

        public TouchscreenInput(PauseHandler pauseHandler) =>
            _pauseHandler = pauseHandler;

        public void Tick()
        {
            if (_pauseHandler.IsPaused == true)
                return;
            
            ReadSwipe();
        }

        private void ReadSwipe()
        {
            if (HasAnyTouches == false)
                return;

            Touch touch = Input.GetTouch(PrimaryTouchIndex);

            if (touch.phase == TouchPhase.Began)
                StartSwipe(touch.position);

            if (touch.phase == TouchPhase.Moved && _isSwiping == true)
                Swipe(touch);
        }

        private void StartSwipe(Vector2 initialTouchPosition)
        {
            _initialTouchPosition = initialTouchPosition;
            _isSwiping = true;
        }

        private void Swipe(Touch touch)
        {
            Vector2 delta = touch.position - _initialTouchPosition;

            if (IsThresholdReached(delta.sqrMagnitude) == false)
                return;

            Direction direction = Mathf.Abs(delta.x) > Mathf.Abs(delta.y) ?
                delta.x < 0 ? Direction.Left : Direction.Right :
                delta.y < 0 ? Direction.Down : Direction.Up;

            DirectionReceived?.Invoke(direction);
            _isSwiping = false;
        }

        private bool IsThresholdReached(float squaredDistance) =>
            squaredDistance >= SwipeThreshold * SwipeThreshold;
    }
}
