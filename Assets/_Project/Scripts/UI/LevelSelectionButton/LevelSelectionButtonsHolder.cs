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
        [SerializeField] private ScrollableMenu _scrollableMenu;
        
        private List<LevelSelectionButton> _selectionButtons = new List<LevelSelectionButton>();

        private LevelSelectionButtonFactory _buttonFactory;
        private SceneLoader _sceneLoader;
        private SavedProgressStorage _progressStorage;
        private LevelSelectionButton _followedSelectionButton;

        [Inject]
        private void Construct(LevelSelectionButtonFactory buttonFactory, SceneLoader sceneLoader, SavedProgressStorage progressStorage)
        {
            _buttonFactory = buttonFactory;
            _sceneLoader = sceneLoader;
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

        public void Load(SavedProgress progress)
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

        public void Save(SavedProgress progress)
        {
            int activeSceneBuildIndex = _sceneLoader.ActiveScene.BuildIndex;
            progress.UnlockedSceneBuildIndexes.Add(activeSceneBuildIndex);

            if (_followedSelectionButton.LevelToLoad.BuildIndex == activeSceneBuildIndex)
                FollowFirstLockedSelectionButton();
        }

        private void LoadScene(LevelSelectionButton selectionButton)
        {
            if (selectionButton.State == LevelSelectionButtonState.Locked)
                return;

            _sceneLoader.LoadActiveSceneAsync(selectionButton.LevelToLoad, () => _scrollableMenu.Hide(true)).Forget();
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
