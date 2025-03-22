using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(ScrollableMenu))]
    public class ScrollableMenuNavigation : MonoBehaviour
    {
        [SerializeField] private ScrollableMenu _scrollableMenu;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        private void Awake()
        {
            _scrollableMenu.TabScrolled += ToggleButtonsView;

            _leftButton.onClick.AddListener(ScrollToLeftTab);
            _rightButton.onClick.AddListener(ScrollToRightTab);
        }

        private void OnDestroy()
        {
            _scrollableMenu.TabScrolled -= ToggleButtonsView;

            _leftButton.onClick.RemoveListener(ScrollToLeftTab);
            _rightButton.onClick.RemoveListener(ScrollToRightTab);
        }

        private void ScrollToLeftTab()
        {
            int leftTabIndex = _scrollableMenu.CurrentTabIndex - 1;
            _scrollableMenu.SnapToTab(leftTabIndex);
        }

        private void ScrollToRightTab()
        {
            int rightTabIndex = _scrollableMenu.CurrentTabIndex + 1;
            _scrollableMenu.SnapToTab(rightTabIndex);
        }

        private void ToggleButtonsView()
        {
            _leftButton.gameObject.SetActive(_scrollableMenu.IsFirstTabOpened == false);
            _rightButton.gameObject.SetActive(_scrollableMenu.IsLastTabOpened == false);
        }
    }
}
