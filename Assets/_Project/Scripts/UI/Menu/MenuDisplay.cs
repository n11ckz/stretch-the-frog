using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Project
{
    public class MenuDisplay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField, Range(0.0f, 0.3f)] private float _totalDuration;
        [SerializeField] private Vector2 _anchoredOffsetPosition;
        [SerializeField] private Ease _ease;

        public bool IsHidden => gameObject.activeInHierarchy == false;

        private bool IsAnimationPlaying => _animationSequence != null && _animationSequence.IsActive() == true;

        private PauseHandler _pauseHandler;
        private RectTransform _rectTransform;
        private Sequence _animationSequence;
        private Vector2 _initialAnchoredPosition;

        [Inject]
        private void Construct(PauseHandler pauseHandler) =>
            _pauseHandler = pauseHandler;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;

            _initialAnchoredPosition = _rectTransform.anchoredPosition;
            _canvasGroup.alpha = 0.0f;
        }

        public async UniTaskVoid Show()
        {
            if (IsAnimationPlaying == true)
                _animationSequence.Complete();

            gameObject.Activate();
            _pauseHandler.Pause();

            _animationSequence = DOTween.Sequence().
                Join(_rectTransform.DOAnchorPos(_initialAnchoredPosition, _totalDuration)).
                Join(_canvasGroup.DOFade(1.0f, _totalDuration)).
                SetEase(_ease).
                SetUpdate(true);

            await _animationSequence.ToUniTask();

            _canvasGroup.interactable = true;
        }

        public async UniTaskVoid Hide()
        {
            if (IsAnimationPlaying == true)
                _animationSequence.Complete();

            _canvasGroup.interactable = false;
            _pauseHandler.Resume();

            _animationSequence = DOTween.Sequence().
                Join(_rectTransform.DOAnchorPos(_anchoredOffsetPosition, _totalDuration)).
                Join(_canvasGroup.DOFade(0.0f, _totalDuration)).
                SetEase(_ease).
                SetUpdate(true);

            await _animationSequence.ToUniTask();

            gameObject.Deactivate();
        }

        public void HideImmediately()
        {
            if (IsAnimationPlaying == true)
                _animationSequence.Kill();

            _rectTransform.anchoredPosition = _anchoredOffsetPosition;
            _canvasGroup.alpha = 0.0f;
            _pauseHandler.Resume();

            gameObject.Deactivate();
        }
    }
}
