using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(ScrollableMenu))]
    public class ScrollableMenuLayout : MonoBehaviour
    {
        public int EnabledTabsCount { get; private set; }
        public IEnumerable<ScrollableTab> Tabs => _tabs;

        private RectTransform _content;
        private ScrollableTab[] _tabs;

        public void Initialize(RectTransform content)
        {
            _content = content;
            _tabs = content.GetComponentsInChildren<ScrollableTab>();

            RebuildContent();
        }

        public void RebuildContent()
        {
            EnabledTabsCount = 0;
            Vector2 size = new Vector2(0.0f, _content.sizeDelta.y);
            
            foreach (ScrollableTab tab in _tabs)
            {
                if (tab.IsDisabled == true)
                    continue;

                tab.Index = EnabledTabsCount;
                size.x += tab.RectTransform.sizeDelta.x;
                EnabledTabsCount++;
            }

            _content.sizeDelta = size;
        }
    }
}
