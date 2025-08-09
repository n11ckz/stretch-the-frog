using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(BumpEffect), typeof(CharacterSoundPlayer))]
    public class SlideMovement : MonoBehaviour, IMovement
    {
        public event Action<Vector3> MovedAt;

        [SerializeField] private CharacterSoundPlayer _soundPlayer;
        [SerializeField] private BumpEffect _bumpEffect;

        public DirectionInfo DirectionInfo => _directionInfo;

        private ObstacleSensor _obstacleSensor;
        private TraceFactory _traceFactory;
        private CharacterConfig _config;
        private CancellationTokenSource _cancellationTokenSource;
        private DirectionInfo _directionInfo;
        private bool _isMoving;

        [Inject]
        private void Construct(ObstacleSensor obstacleSensor, TraceFactory traceFactory, CharacterConfig config)
        {
            _obstacleSensor = obstacleSensor;
            _traceFactory = traceFactory;
            _config = config;
        }

        private void Awake() =>
            CreateCancellationTokenSource();

        public void Move(Direction direction)
        {
            Vector3 convertedDirection = direction.ToVector();

            if (CanMove(convertedDirection) == false)
                return;

            if (_cancellationTokenSource.IsCancellationRequested == true)
                CreateCancellationTokenSource();

            _directionInfo.Current = direction;
            MoveAsync(convertedDirection, _cancellationTokenSource.Token).Forget();
        }

        public void StopMove()
        {
            if (_cancellationTokenSource.IsCancellationRequested == false)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }

            _isMoving = false;
        }

        private async UniTaskVoid MoveAsync(Vector3 direction, CancellationToken cancellationToken)
        {
            _isMoving = true;

            while (_obstacleSensor.HasObstacleAhead(transform.position, direction) == false)
            {
                LeaveTraceOnWay();

                Vector3 destination = transform.position + direction;

                await transform.DOMove(destination, 1.0f / _config.Speed).SetEase(Ease.Linear).
                    SetLink(gameObject).ToUniTask(TweenCancelBehaviour.CancelAwait, cancellationToken);

                _directionInfo.Previous = _directionInfo.Current;
                MovedAt?.Invoke(destination);

                await UniTask.WaitForEndOfFrame(cancellationToken);
            }

            _bumpEffect.PlayEffect(direction);
            _isMoving = false;
        }

        private void LeaveTraceOnWay()
        {
            Trace trace = _traceFactory.CreateTrace(_directionInfo);
            trace.transform.position = transform.position;
        }

        private void CreateCancellationTokenSource()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.RegisterRaiseCancelOnDestroy(gameObject);
        }

        private bool CanMove(Vector3 direction)
        {
            if (_isMoving == true)
                return false;

            bool hasObstacle = _obstacleSensor.HasObstacleAhead(transform.position, direction);

            if (hasObstacle == true)
                _bumpEffect.PlaySlimEffect(direction);

            return hasObstacle == false;
        }
    }
}
