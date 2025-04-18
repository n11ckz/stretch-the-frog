using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class ScreenCurtain : MonoBehaviour
    {
        [SerializeField] private Image _curtainImage;

        [SerializeField, Range(0.0f, 0.5f)] private float _fadeDuration;
        [SerializeField, Range(0.0f, 0.25f)] private float _delayBeforeHide;
        [SerializeField] private Ease _ease;

        public async UniTask Show()
        {
            _curtainImage.gameObject.Enable();

            await _curtainImage.DOFade(1.0f, _fadeDuration).
                SetEase(_ease).
                SetUpdate(true).
                ToUniTask();
        }

        public async UniTaskVoid Hide()
        {
            await _curtainImage.DOFade(0.0f, _fadeDuration).
                SetEase(_ease).
                SetDelay(_delayBeforeHide).
                SetUpdate(true).
                ToUniTask();

            _curtainImage.gameObject.Disable();
        }
    }
}
