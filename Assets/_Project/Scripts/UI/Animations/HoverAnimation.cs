using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
    public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Min(1.0f)] private float _scale;
        [SerializeField, Range(0.0f, 0.2f)] private float _duration;
        [SerializeField] private Ease _ease;

        private Tween _tween;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween?.Complete();
            _tween = transform.DOScale(_scale, _duration).
                SetEase(_ease).
                SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tween?.Complete();
            _tween = transform.DOScale(1.0f, _duration).
                SetEase(_ease).
                SetUpdate(true);
        }
    }
}
