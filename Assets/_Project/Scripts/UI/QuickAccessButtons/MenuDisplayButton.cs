using UnityEngine;

namespace Project
{
    public class MenuDisplayButton : BaseQuickAccessButton
    {
        [SerializeField] private ScrollMenu _scrollMenu;

        protected override void Execute()
        {
            if (_scrollMenu.IsHidden == true)
                _scrollMenu.OpenTab();
            else
                _scrollMenu.Hide();
        }
    }
}
