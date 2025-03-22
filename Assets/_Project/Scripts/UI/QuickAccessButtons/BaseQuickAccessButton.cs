using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseQuickAccessButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Execute);
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(Execute);

        protected abstract void Execute();
    }
}
