using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class ScreenCurtain : MonoBehaviour
    {
        [Title("Components")]
        [SerializeField] private Image _curtainImage;

        [Title("Animation Settings")]
        [SerializeField, Range(0.0f, 0.5f)] private float _fadeDuration;
        [SerializeField, Range(0.0f, 0.25f)] private float _delayBeforeHide;
        [SerializeField] private Ease _ease;

        public async UniTask Show()
        {
            _curtainImage.gameObject.Activate();

            await _curtainImage.DOFade(1.0f, _fadeDuration).SetEase(_ease).SetUpdate(true).
                ContinueWith(() => UniTask.WaitForEndOfFrame(this)); // Idk, but without this, DOTween.KillAll() throws weird error

            DOTween.KillAll();
        }

        public async UniTaskVoid Hide()
        {
            await _curtainImage.DOFade(0.0f, _fadeDuration).SetEase(_ease).SetDelay(_delayBeforeHide).SetUpdate(true);

            _curtainImage.gameObject.Deactivate();
        }
    }
}
