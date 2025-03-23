using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(ScrollRect), typeof(ScrollMenuLayoutRebuilder), typeof(ScrollMenuDisplay))]
    public class ScrollMenu : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public event Action TabOpened;

        [SerializeField] private ScrollRect _rect;
        [SerializeField] private ScrollMenuLayoutRebuilder _layout;
        [SerializeField] private ScrollMenuDisplay _display;

        [SerializeField, Range(25.0f, 300.0f)] private float _swipeThreshold;
        [SerializeField, Range(0.0f, 0.5f)] private float _scrollDuration;
        [SerializeField] private Ease _scrollEase;

        public int OpenedTabIndex { get; private set; }
        public int TabsCount => _tabs.Count;
        public bool IsHidden => gameObject.activeInHierarchy == false;

        private List<ScrollableTab> _tabs;
        private Tween _scrollTween;

        public void Initialize()
        {
            _tabs = _rect.content.GetComponentsInChildren<ScrollableTab>().ToList();
            _display.Initialize();
        }

        public void OnBeginDrag(PointerEventData eventData) =>
            _scrollTween?.Complete();

        public void OnEndDrag(PointerEventData eventData)
        {
            float delta = eventData.position.x - eventData.pressPosition.x;
            int targetTabIndex = Mathf.Abs(delta) > _swipeThreshold ? OpenedTabIndex - (int)Mathf.Sign(delta) : OpenedTabIndex;
            ScrollToTab(targetTabIndex);
        }

        public void OpenTab(ScrollableTabType tabType = ScrollableTabType.LevelSelection)
        {
            if (TryFindTab(tabType, out ScrollableTab findedTab) == false)
                return;

            int findedTabIndex = _tabs.IndexOf(findedTab);
            _rect.horizontalNormalizedPosition = findedTabIndex / ((float)TabsCount - 1);
            _display.Show().Forget();

            SetTabIndex(findedTabIndex);
        }

        public void Hide(bool isImmediate = false) =>
            _display.Hide(isImmediate).Forget();

        public void ScrollToTab(int targetTabIndex)
        {
            if (targetTabIndex < 0 || targetTabIndex >= TabsCount)
                return;

            float normalizedTabPosition = targetTabIndex / ((float)TabsCount - 1);
            _scrollTween = _rect.DOHorizontalNormalizedPos(normalizedTabPosition, _scrollDuration).
                SetEase(_scrollEase).
                SetUpdate(true);

            SetTabIndex(targetTabIndex);
        }

        public void InsertTab(int childIndex, ScrollableTab tab)
        {
            tab.transform.SetParentAndSiblingIndex(_rect.content, childIndex);

            _tabs.Insert(childIndex, tab);
            _layout.RebuildContent();
        }

        public void RemoveTab(ScrollableTabType tabType, Action<ScrollableTab> onTabRemoved)
        {
            if (TryFindTab(tabType, out ScrollableTab findedTab) == false)
                return;

            _tabs.Remove(findedTab);
            onTabRemoved.Invoke(findedTab);
            _layout.RebuildContent();
        }

        private void SetTabIndex(int index)
        {
            OpenedTabIndex = index;
            TabOpened?.Invoke();
        }

        private bool TryFindTab(ScrollableTabType tabType, out ScrollableTab findedTab)
        {
            findedTab = _tabs.FirstOrDefault((tab) => tab.Type == tabType);
            return findedTab != null;
        }
    }
}
