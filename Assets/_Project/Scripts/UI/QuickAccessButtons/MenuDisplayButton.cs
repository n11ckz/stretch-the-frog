using UnityEngine;

namespace Project
{
    public class MenuDisplayButton : BaseQuickAccessButton
    {
        [SerializeField] private ScrollableMenu _scrollableMenu;

        protected override void Execute()
        {
            if (_scrollableMenu.gameObject.activeInHierarchy == false)
            {
                _scrollableMenu.OpenTab();
            }
            else
            {
                _scrollableMenu.Hide(false);
            }
        }
    }
}
