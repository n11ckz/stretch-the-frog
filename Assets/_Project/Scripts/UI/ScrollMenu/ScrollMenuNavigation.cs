using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(ScrollMenu))]
    public class ScrollMenuNavigation : MonoBehaviour
    {
        [SerializeField] private ScrollMenu _scrollableMenu;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        private void Awake()
        {
            _scrollableMenu.TabOpened += ToggleButtonsDisplay;

            _leftButton.onClick.AddListener(ScrollToLeftTab);
            _rightButton.onClick.AddListener(ScrollToRightTab);
        }

        private void OnDestroy()
        {
            _scrollableMenu.TabOpened -= ToggleButtonsDisplay;

            _leftButton.onClick.RemoveListener(ScrollToLeftTab);
            _rightButton.onClick.RemoveListener(ScrollToRightTab);
        }

        private void ScrollToLeftTab()
        {
            int leftTabIndex = _scrollableMenu.OpenedTabIndex - 1;
            _scrollableMenu.ScrollToTab(leftTabIndex);
        }

        private void ScrollToRightTab()
        {
            int rightTabIndex = _scrollableMenu.OpenedTabIndex + 1;
            _scrollableMenu.ScrollToTab(rightTabIndex);
        }

        private void ToggleButtonsDisplay()
        {
            _leftButton.gameObject.SetActive(_scrollableMenu.OpenedTabIndex != 0);
            _rightButton.gameObject.SetActive(_scrollableMenu.OpenedTabIndex != _scrollableMenu.TabsCount - 1);
        }
    }
}
