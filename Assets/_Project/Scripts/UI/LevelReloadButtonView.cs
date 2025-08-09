using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project
{
    public class LevelReloadButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private LevelLoader _levelLoader;

        [Inject]
        private void Construct(LevelLoader levelLoader) =>
            _levelLoader = levelLoader;

        private void Awake() =>
            _button.onClick.AddListener(_levelLoader.ReloadCurrentLevel);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(_levelLoader.ReloadCurrentLevel);
    }
}
