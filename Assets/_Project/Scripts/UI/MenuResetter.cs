using Alchemy.Inspector;
using UnityEngine;

namespace Project
{
    public class MenuResetter : MonoBehaviour
    {
        [SerializeField] private ScrollMenu _scrollMenu;
        [SerializeField] private CompletionTabHandler _completionTabHandler;

        [Button]
        public void ResetElements()
        {
            _completionTabHandler.DisableTab();
            _scrollMenu.Hide(true);
        }
    }
}
