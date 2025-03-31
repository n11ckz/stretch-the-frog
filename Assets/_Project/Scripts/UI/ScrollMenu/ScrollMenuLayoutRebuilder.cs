using UnityEngine;

namespace Project
{
    [ExecuteAlways, RequireComponent(typeof(ScrollMenu))]
    public class ScrollMenuLayoutRebuilder : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField, Min(0)] private int _preferredWidth;

        public float WidthRatio => (float)Screen.width / _preferredWidth;

        private RectTransform _rectTransform;

        private void OnRectTransformDimensionsChange()
        {
            _rectTransform = transform as RectTransform;
            RebuildContent();
        }

        public void RebuildContent()
        {
            float contentWidth = 0.0f;
            Vector2 size = _rectTransform.rect.size;

            foreach (RectTransform childRectTransform in _content)
            {
                contentWidth += size.x;
                childRectTransform.sizeDelta = size;
            }

            _content.sizeDelta = new Vector2(contentWidth, _content.sizeDelta.y);
        }
    }
}
