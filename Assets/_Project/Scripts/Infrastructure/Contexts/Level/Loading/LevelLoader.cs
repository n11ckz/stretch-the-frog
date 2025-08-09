using n11ckz.SceneReference;
using UnityEngine;

namespace Project
{
    public class LevelLoader
    {
        private readonly LevelCycle _levelCycle;
        private readonly LevelSequence _levelSequence;
        private readonly SceneLoader _sceneLoader;

        private int _currentLevelIndex = -1;

        public LevelLoader(LevelCycle levelCycle, LevelSequence levelSequence, SceneLoader sceneLoader)
        {
            _levelCycle = levelCycle;
            _levelSequence = levelSequence;
            _sceneLoader = sceneLoader;
        }

        public void LoadNextLevelInSequence()
        {
            _currentLevelIndex = (_currentLevelIndex + 1) % _levelSequence.Levels.Count;
            _sceneLoader.LoadActiveSceneAsync(_levelSequence[_currentLevelIndex],
                Application.exitCancellationToken, () => _levelCycle.Dispose()).Forget();
        }

        public void LoadLevelBySceneReference(SceneReference sceneReference)
        {
            _currentLevelIndex = _levelSequence.Levels.FindIndex((x) => x.BuildIndex == sceneReference.BuildIndex);
            _sceneLoader.LoadActiveSceneAsync(sceneReference, Application.exitCancellationToken,
                () => _levelCycle.Dispose()).Forget();
        }

        public void ReloadCurrentLevel() =>
            _sceneLoader.LoadActiveSceneAsync(_levelSequence[_currentLevelIndex],
                Application.exitCancellationToken, () => _levelCycle.Dispose()).Forget();
    }
}
