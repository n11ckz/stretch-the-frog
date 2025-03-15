using UnityEngine;

namespace Project
{
    public class MenuDisplayButton : BaseQuickAccessButton
    {
        [SerializeField] private MenuDisplay _menuDisplay;

        protected override void Execute()
        {
            if (_menuDisplay.IsHidden == true)
            {
                _menuDisplay.Show().Forget();
            }
            else
            {
                _menuDisplay.Hide().Forget();
            }
        }
    }
}
