using UnityEngine;

namespace Project
{
    public class MenuBootstrap : MonoBehaviour
    {
        [SerializeField] private MenuDisplay _menuDisplay;
        [SerializeField] private LevelSelectionButtonsHolder _selectionButtonsHolder;

        private void Start()
        {
            _selectionButtonsHolder.Initialize();

            _menuDisplay.HideImmediately();
        }
    }
}
