using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class LoadNextLevelButton : BaseQuickAccessButton
    {
        [SerializeField] private ScrollableMenu _scrollableMenu;

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
            _sceneLoader.LoadActiveSceneAsync(levelSceneReference, () => _scrollableMenu.Hide(true)).Forget();
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
