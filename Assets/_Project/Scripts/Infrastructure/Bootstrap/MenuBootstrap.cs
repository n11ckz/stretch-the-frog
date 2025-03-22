using UnityEngine;

namespace Project
{
    public class MenuBootstrap : MonoBehaviour
    {
        [SerializeField] private ScrollableMenu _scrollableMenu;
        [SerializeField] private LevelSelectionButtonsHolder _selectionButtonsHolder;

        private void Start()
        {
            _selectionButtonsHolder.Initialize();
            _scrollableMenu.Initialize();

            _scrollableMenu.Hide(true);
        }
    }
}
