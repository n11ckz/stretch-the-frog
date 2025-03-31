using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class LoadNextSceneButton : BaseQuickAccessButton
    {
        [SerializeField] private MenuResetter _menuResetter;

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
            SceneReference findedScene = FindNextSceneInSequence();
            _sceneLoader.LoadActiveSceneAsync(findedScene, () => _menuResetter.ResetElements()).Forget();
        }

        private SceneReference FindNextSceneInSequence()
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
