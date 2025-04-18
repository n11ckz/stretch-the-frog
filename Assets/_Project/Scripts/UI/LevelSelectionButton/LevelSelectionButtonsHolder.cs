using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project
{
    public class LevelSelectionButtonsHolder : MonoBehaviour, IProgressListener
    {
        [SerializeField] private RectTransform _buttonsParent;
        [SerializeField] private MenuResetter _menuResetter;
        
        private List<LevelSelectionButton> _selectionButtons = new List<LevelSelectionButton>();

        private SceneLoader _sceneLoader;
        private LevelSelectionButtonFactory _buttonFactory;
        private ProgressStorage _progressStorage;
        private LevelSelectionButton _followedSelectionButton;

        [Inject]
        private void Construct(SceneLoader sceneLoader, LevelSelectionButtonFactory buttonFactory, ProgressStorage progressStorage)
        {
            _sceneLoader = sceneLoader;
            _buttonFactory = buttonFactory;
            _progressStorage = progressStorage;
        }

        private void OnDestroy() =>
            _progressStorage.RemoveListener(this);

        public void Initialize()
        {
            _progressStorage.AddListener(this);
            
            foreach (LevelSelectionButton selectionButton in _buttonFactory.CreateButtonsLazy())
            {
                selectionButton.transform.SetParent(_buttonsParent, false);
                selectionButton.AddListener(() => LoadScene(selectionButton));

                _selectionButtons.Add(selectionButton);
            }
        }

        public void Load(Progress progress)
        {
            foreach (LevelSelectionButton selectionButton in _selectionButtons)
            {
                int levelSceneBuildIndex = selectionButton.LevelToLoad.BuildIndex;

                if (progress.UnlockedSceneBuildIndexes.Contains(levelSceneBuildIndex) == false)
                    continue;

                selectionButton.Unlock();
            }

            FollowFirstLockedSelectionButton();
        }

        public void Save(Progress progress)
        {
            int activeSceneBuildIndex = _sceneLoader.ActiveScene.BuildIndex;
            progress.UnlockedSceneBuildIndexes.Add(activeSceneBuildIndex);

            if (_followedSelectionButton != null && _followedSelectionButton.LevelToLoad.BuildIndex == activeSceneBuildIndex)
                FollowFirstLockedSelectionButton();
        }

        private void LoadScene(LevelSelectionButton selectionButton)
        {
            if (selectionButton.State == LevelSelectionButtonState.Locked)
                return;

            _sceneLoader.LoadActiveSceneAsync(selectionButton.LevelToLoad, () => _menuResetter.ResetElements()).Forget();
        }

        private void FollowFirstLockedSelectionButton()
        {
            _followedSelectionButton?.Unfollow();

            if (TryFindSelectionButtonForFollow(out LevelSelectionButton followedSelectionButton) == false)
                return;

            _followedSelectionButton = followedSelectionButton;
            _followedSelectionButton.Follow();
        }

        private bool TryFindSelectionButtonForFollow(out LevelSelectionButton selectionButton)
        {
            selectionButton = _selectionButtons.FirstOrDefault((button) => button.State == LevelSelectionButtonState.Locked);

            return selectionButton != null;
        }
    }
}
