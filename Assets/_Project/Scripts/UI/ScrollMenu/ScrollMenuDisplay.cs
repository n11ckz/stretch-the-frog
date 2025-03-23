using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(ScrollMenu), typeof(CanvasGroup))]
    public class ScrollMenuDisplay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField, Range(0.0f, 0.3f)] private float _totalDuration;
        [SerializeField] private Vector2 _anchoredOffsetPosition;
        [SerializeField] private Ease _ease;

        private PauseHandler _pauseHandler;
        private RectTransform _rectTransform;
        private Sequence _animationSequence;
        private Vector2 _initialAnchoredPosition;

        [Inject]
        private void Construct(PauseHandler pauseHandler) =>
            _pauseHandler = pauseHandler;

        public void Initialize()
        {
            _rectTransform = transform as RectTransform;
            _initialAnchoredPosition = _rectTransform.anchoredPosition;
            _canvasGroup.alpha = 0.0f;
        }

        public async UniTaskVoid Show()
        {
            _animationSequence?.Complete();
            _pauseHandler.Pause();
            gameObject.Enable();

            _animationSequence = DOTween.Sequence().
                Join(_rectTransform.DOAnchorPos(_initialAnchoredPosition, _totalDuration)).
                Join(_canvasGroup.DOFade(1.0f, _totalDuration)).
                SetEase(_ease).
                SetUpdate(true);

            await _animationSequence.ToUniTask();

            _canvasGroup.interactable = true;
        }

        public async UniTaskVoid Hide(bool isImmediate)
        {
            _animationSequence?.Complete();
            _pauseHandler.Resume();
            _canvasGroup.interactable = false;

            float sequenceDuration = isImmediate == true ? 0.0f : _totalDuration;
            _animationSequence = DOTween.Sequence().
                Join(_rectTransform.DOAnchorPos(_anchoredOffsetPosition, sequenceDuration)).
                Join(_canvasGroup.DOFade(0.0f, sequenceDuration)).
                SetEase(_ease).
                SetUpdate(true);

            await _animationSequence.ToUniTask();

            gameObject.Disable();
        }
    }
}
