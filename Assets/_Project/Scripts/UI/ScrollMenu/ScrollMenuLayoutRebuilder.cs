using UnityEngine;

namespace Project
{
    [ExecuteAlways, RequireComponent(typeof(ScrollMenu))]
    public class ScrollMenuLayoutRebuilder : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;

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
