using UnityEngine;

namespace Project
{
    public class CompletionTab : ScrollableTab
    {
        [SerializeField] private RectTransform _successfullySetup;
        [SerializeField] private RectTransform _failedSetup;

        public void EnableSetup(bool isSuccessfully)
        {
            gameObject.Enable();

            if (isSuccessfully == true)
            {
                _successfullySetup.gameObject.Enable();
                _failedSetup.gameObject.Disable();
            }
            else
            {
                _successfullySetup.gameObject.Disable();
                _failedSetup.gameObject.Enable();
            }
        }
    }
}
