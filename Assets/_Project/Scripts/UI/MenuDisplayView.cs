using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Project
{
    public class MenuDisplayView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Min(0.0f)] private float _duration;

        private Tween _sequence;

        public async UniTaskVoid Show(CancellationToken cancellationToken)
        {
            _sequence?.Complete();
            _canvasGroup.gameObject.Enable();

            await _canvasGroup.DOFade(1.0f, _duration).SetEase(Ease.InOutSine).
                WriteInField(ref _sequence).SetUpdate(true).SetLink(_canvasGroup.gameObject).
                ToUniTask(TweenCancelBehaviour.Kill, cancellationToken);

            _canvasGroup.interactable = true;
        }

        public async UniTaskVoid Hide(CancellationToken cancellationToken)
        {
            _sequence?.Complete();
            _canvasGroup.interactable = false;

            await _canvasGroup.DOFade(0.0f, _duration).SetEase(Ease.InOutSine).
                WriteInField(ref _sequence).SetUpdate(true).SetLink(_canvasGroup.gameObject).
                ToUniTask(TweenCancelBehaviour.Kill, cancellationToken);

            _canvasGroup.gameObject.Disable();
        } 
    }
}
