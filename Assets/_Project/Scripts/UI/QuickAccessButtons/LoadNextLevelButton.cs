using UnityEngine;
using Zenject;

namespace Project
{
    public class LoadNextLevelButton : BaseQuickAccessButton
    {
        [SerializeField] private SceneLoaderFacade _sceneLoaderFacade;

        private LevelSequence _levelSequence;

        [Inject]
        private void Construct(LevelSequence levelSequence) =>
            _levelSequence = levelSequence;

        protected override void Execute()
        {
            SceneReference findedLevel = FindNextLevelInSequence();
            _sceneLoaderFacade.LoadActiveScene(findedLevel);
        }

        private SceneReference FindNextLevelInSequence()
        {
            int levelCount = _levelSequence.LevelReferences.Count;

            for (int i = 0; i < levelCount; i++)
            {
                if (_levelSequence.LevelReferences[i].BuildIndex != _sceneLoaderFacade.ActiveSceneBuildIndex)
                    continue;

                return _levelSequence.LevelReferences[(i + 1) % levelCount];
            }

            return _levelSequence.InitialLevelReference;
        }
    }
}
