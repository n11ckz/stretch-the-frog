using UnityEngine;

namespace Project
{
    public class MenuBootstrap : MonoBehaviour
    {
        [SerializeField] private ScrollMenu _scrollableMenu;
        [SerializeField] private CompletionTabHandler _completionTabHandler;
        [SerializeField] private LevelSelectionButtonsHolder _selectionButtonsHolder;

        private void Start()
        {
            _selectionButtonsHolder.Initialize();
            _scrollableMenu.Initialize();
            _completionTabHandler.Initialize();

            _scrollableMenu.Hide(true);
        }
    }
}
