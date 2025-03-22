using Alchemy.Inspector;
using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(ScrollRect), typeof(ScrollableMenuLayout), typeof(ScrollableMenuDisplay))]
    public class ScrollableMenu : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public event Action TabScrolled;

        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private ScrollableMenuLayout _layout;
        [SerializeField] private ScrollableMenuDisplay _display;

        [SerializeField, Range(10.0f, 300.0f)] private float _swipeThreshold;
        [SerializeField, Range(0.0f, 0.5f)] private float _snapDuration;
        [SerializeField] private Ease _snapEase;

        public int CurrentTabIndex { get; private set; }
        public bool IsFirstTabOpened => CurrentTabIndex == 0;
        public bool IsLastTabOpened => CurrentTabIndex == _layout.EnabledTabsCount - 1;

        private Tween _scrollTween;

        public void Initialize()
        {
            _layout.Initialize(_scrollRect.content);
            _display.Initialize();
        }

        public void OnBeginDrag(PointerEventData eventData) =>
            _scrollTween?.Complete();

        public void OnEndDrag(PointerEventData eventData)
        {
            float delta = eventData.position.x - eventData.pressPosition.x;

            if (Mathf.Abs(delta) < _swipeThreshold)
            {
                SnapToTab(CurrentTabIndex);
                return;
            }

            int tabIndex = CurrentTabIndex - Math.Sign(delta);
            SnapToTab(tabIndex);
        }

        public void OpenTab(ScrollableTabType tabType = ScrollableTabType.Tab1)
        {
            ScrollableTab scrollableTab = _layout.Tabs.FirstOrDefault((tab) => tab.Type == tabType);

            if (scrollableTab == null || scrollableTab.IsDisabled == true)
                return;

            float normalizedPosition = scrollableTab.Index / ((float)_layout.EnabledTabsCount - 1);
            _scrollRect.horizontalNormalizedPosition = normalizedPosition;
            CurrentTabIndex = scrollableTab.Index;
            _display.Show().Forget();
            TabScrolled?.Invoke();
        }

        public void Hide(bool isImmediate)
        {
            if (isImmediate == false)
                _display.Hide().Forget();
            else
                _display.HideImmediately();
        }

        public void SnapToTab(int tabIndex)
        {
            if (tabIndex < 0 || tabIndex >= _layout.EnabledTabsCount)
                return;

            CurrentTabIndex = tabIndex;

            float normalizedPosition = CurrentTabIndex / ((float)_layout.EnabledTabsCount - 1);
            _scrollTween = _scrollRect.DOHorizontalNormalizedPos(normalizedPosition, _snapDuration).SetEase(_snapEase).SetUpdate(true);
            TabScrolled?.Invoke();
        }

        [Button]
        public void DisableTab(ScrollableTabType tabType)
        {
            ScrollableTab scrollableTab = _layout.Tabs.FirstOrDefault((tab) => tab.Type == tabType);

            if (scrollableTab == null || scrollableTab.IsDisabled == true)
                return;

            if (IsLastTabOpened == true)
                CurrentTabIndex--;

            scrollableTab.gameObject.Deactivate();
            _layout.RebuildContent();
            TabScrolled?.Invoke();
        }
    }
}
