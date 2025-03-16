using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class LoadNextLevelButton : BaseQuickAccessButton
    {
        [SerializeField] private MenuDisplay _menuDisplay;
        [SerializeField] private LevelSelectionButtonsHolder _selectionButtonsHolder;
        [SerializeField] private CompletionPanel _completionPanel;

        private SceneLoader _sceneLoader;
        private LevelSequence _levelSequence;

        [Inject]
        private void Construct(SceneLoader sceneLoader, LevelSequence levelSequence)
        {
            _sceneLoader = sceneLoader;
            _levelSequence = levelSequence;
        }

        protected override void Execute()
        {
            SceneReference levelSceneReference = FindNextLevelInSequence();
            _sceneLoader.LoadActiveSceneAsync(levelSceneReference, () => HideAllMenu()).Forget();
        }

        private void HideAllMenu()
        {
            _selectionButtonsHolder.gameObject.Activate();
            _completionPanel.gameObject.Deactivate();
            _menuDisplay.HideImmediately();
        }

        private SceneReference FindNextLevelInSequence()
        {
            int levelCount = _levelSequence.LevelReferences.Count;

            for (int i = 0; i < levelCount; i++)
            {
                if (_levelSequence.LevelReferences[i].BuildIndex != _sceneLoader.ActiveScene.BuildIndex)
                    continue;

                return _levelSequence.LevelReferences[(i + 1) % levelCount];
            }

            return _levelSequence.InitialLevelReference;
        }
    }
}
