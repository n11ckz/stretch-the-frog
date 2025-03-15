using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace Project
{
    public class SceneLoader
    {
        private readonly ILogger _logger;
        private readonly ScreenCurtain _screenCurtain;

        public SceneReference ActiveScene { get; private set; }

        private bool _isSceneLoading;

        public SceneLoader(ILogger logger, ScreenCurtain screenCurtain)
        {
            _logger = logger;
            _screenCurtain = screenCurtain;
        }

        public async UniTask LoadActiveSceneAsync(SceneReference scene, Action onSceneUnloaded = null)
        {
            if (IsSceneValid(scene) == false || _isSceneLoading == true)
                return;

            _isSceneLoading = true;

            await _screenCurtain.Show().
                ContinueWith(() => UnloadActiveSceneAsync(onSceneUnloaded)).
                ContinueWith(() => LoadAdditionalAsync(scene));

            SetActiveScene(scene);
            _isSceneLoading = false;
            _screenCurtain.Hide().Forget();
        }

        public async UniTask LoadAdditionalAsync(SceneReference scene)
        {
            if (IsSceneValid(scene) == false)
                return;

            await SceneManager.LoadSceneAsync(scene.BuildIndex, LoadSceneMode.Additive);
        }

        public async UniTask ReloadActiveSceneAsync(Action onSceneUnloaded = null) =>
            await LoadActiveSceneAsync(ActiveScene, onSceneUnloaded);

        private async UniTask UnloadActiveSceneAsync(Action onSceneUnloaded)
        {
            int activeSceneBuildIndex = ActiveScene != null ? ActiveScene.BuildIndex : SceneManager.GetActiveScene().buildIndex;

            await SceneManager.UnloadSceneAsync(activeSceneBuildIndex);

            onSceneUnloaded?.Invoke();
        }

        private void SetActiveScene(SceneReference scene)
        {
            Scene activeScene = SceneManager.GetSceneByBuildIndex(scene.BuildIndex);
            SceneManager.SetActiveScene(activeScene);
            ActiveScene = scene;
        }

        private bool IsSceneValid(SceneReference scene)
        {
            bool isSceneValid = scene.BuildIndex != SceneReference.InvalidSceneBuildIndex;

            if (isSceneValid == false)
                _logger.Log($"The <{scene.Name}> not added in build or not selected in inspector");

            return isSceneValid == true;
        }
    }
}
