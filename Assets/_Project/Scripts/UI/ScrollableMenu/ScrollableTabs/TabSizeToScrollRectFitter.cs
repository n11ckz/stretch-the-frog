using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [ExecuteAlways]
    public class TabSizeToScrollRectFitter : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void OnRectTransformDimensionsChange()
        {
            Vector2 size = (transform as RectTransform).rect.size;

            foreach (RectTransform rectTransform in _scrollRect.content)
                rectTransform.sizeDelta = size;
        }
    }
}
