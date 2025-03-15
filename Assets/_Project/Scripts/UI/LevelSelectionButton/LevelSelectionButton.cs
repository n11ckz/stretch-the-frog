using Alchemy.Inspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(Button))]
    public class LevelSelectionButton : MonoBehaviour
    {
        [Title("Components")]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private TMP_Text _levelIndexText;

        public SceneReference LevelToLoad { get; private set; }
        public LevelSelectionButtonState State { get; private set; }

        private Button _button;
        private LevelSelectionButtonConfig _buttonConfig;

        public void Initialize(SceneReference levelToLoad, LevelSelectionButtonConfig buttonConfig, int levelIndex)
        {
            LevelToLoad = levelToLoad;

            _button = GetComponent<Button>();
            _buttonConfig = buttonConfig;
            _levelIndexText.text = levelIndex.ToString();
        }

        public void AddListener(UnityAction onButtonClicked) =>
            _button.onClick.AddListener(onButtonClicked);

        public void Unlock()
        {
            if (State != LevelSelectionButtonState.Locked)
                return;

            _lockImage.gameObject.Deactivate();
            _levelIndexText.gameObject.Activate();

            ChangeState(LevelSelectionButtonState.Unlocked);
        }

        public void Follow()
        {
            if (State == LevelSelectionButtonState.Locked)
                Unlock();

            ChangeState(LevelSelectionButtonState.Followed);
        }

        public void Unfollow()
        {
            if (State != LevelSelectionButtonState.Followed)
                return;

            ChangeState(LevelSelectionButtonState.Unlocked);
        }

        private void ChangeState(LevelSelectionButtonState state)
        {
            _backgroundImage.color = state switch
            {
                LevelSelectionButtonState.Unlocked => _buttonConfig.UnlockedBackgroundColor,
                LevelSelectionButtonState.Followed => _buttonConfig.FollowedBackgroundColor,
                _ => _buttonConfig.LockedBackgroundColor
            };

            State = state;
        }
    }
}
