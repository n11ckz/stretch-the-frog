using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadingScreenCurtain : MonoBehaviour, IScreenCurtain
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField, Min(0.0f)] private float _fadeDuration;
        [SerializeField, Min(0.0f)] private float _delayBeforeHiding;

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            gameObject.Enable();

            await _canvasGroup.DOFade(1.0f, _fadeDuration).SetEase(Ease.InOutSine).
                SetUpdate(true).SetLink(gameObject).ToUniTask(TweenCancelBehaviour.Kill, cancellationToken);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken)
        {
            await _canvasGroup.DOFade(0.0f, _fadeDuration).SetEase(Ease.InOutSine).SetDelay(_delayBeforeHiding).
                SetUpdate(true).SetLink(gameObject).ToUniTask(TweenCancelBehaviour.Kill, cancellationToken);

            gameObject.Disable();
        }
    }
}
