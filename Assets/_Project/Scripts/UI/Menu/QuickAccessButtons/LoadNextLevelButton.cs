using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class LoadNextLevelButton : BaseQuickAccessButton
    {
        [SerializeField] private MenuDisplay _menuDisplay;

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
            SceneReference levelReference = FindNextLevelInSequence();
            _sceneLoader.LoadActiveSceneAsync(levelReference, () => _menuDisplay.HideImmediately()).Forget();
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
