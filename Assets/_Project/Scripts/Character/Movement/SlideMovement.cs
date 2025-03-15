using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(BumpEffect))]
    public class SlideMovement : MonoBehaviour, IMovement
    {
        public event Action<Vector3> MovedAt;
        
        public Direction CurrentDirection => _directionInfo.Current;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private DirectionInfo _directionInfo = new DirectionInfo();

        private ObstacleSensor _sensor;
        private TraceFactory _traceFactory;
        private CharacterConfig _config;
        private BumpEffect _bumpEffect;
        private bool _isMoving;

        [Inject]
        private void Construct(ObstacleSensor sensor, TraceFactory traceFactory, CharacterConfig config)
        {
            _sensor = sensor;
            _traceFactory = traceFactory;
            _config = config;
        }

        private void Awake() =>
            _bumpEffect = GetComponent<BumpEffect>();

        private void OnDestroy() =>
            CancelToken();

        public void Move(Direction direction)
        {
            Vector3 vectorDirection = direction.ToVector();

            if (CanMove(vectorDirection) == false)
                return;

            _cancellationTokenSource ??= new CancellationTokenSource();
            _directionInfo.Current = direction;

            MoveAsync(vectorDirection, _cancellationTokenSource.Token).Forget();
        }

        public void StopMove()
        {
            CancelToken();
            _isMoving = false;
        }

        private async UniTaskVoid MoveAsync(Vector3 direction, CancellationToken token)
        {
            _isMoving = true;

            while (_sensor.HasObstacleAhead(transform.position, direction) == false)
            {
                LeaveTrace();

                Vector3 destination = transform.position + direction;

                await transform.DOMove(destination, _config.MoveDurationPerCell).SetEase(_config.MoveEase).WithCancellation(token);

                _directionInfo.Previous = _directionInfo.Current;
                MovedAt?.Invoke(destination);

                await UniTask.WaitForEndOfFrame(this, token);
            }

            _bumpEffect.PlayEffect(direction);
            _isMoving = false;
        }

        private void LeaveTrace()
        {
            Trace trace = _traceFactory.CreateTrace(_directionInfo);
            trace.transform.position = transform.position.With(y: trace.PositionOffset.y);
        }

        private void CancelToken()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private bool CanMove(Vector3 direction)
        {
            if (_isMoving == true)
                return false;

            bool hasObstacle = _sensor.HasObstacleAhead(transform.position, direction);

            if (hasObstacle == true)
                _bumpEffect.PlaySlimEffect(direction);

            return hasObstacle == false;
        }
    }
}
