using Cysharp.Threading.Tasks;
using n11ckz.SceneReference;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

namespace Project
{
    public class SceneLoader
    {
        private readonly IAdsService _adsService;
        private readonly IScreenCurtain _screenCurtain;

        private bool _isSceneLoading;

        public SceneLoader(IAdsService adsService, IScreenCurtain screenCurtain)
        {
            _adsService = adsService;
            _screenCurtain = screenCurtain;
        }

        public async UniTaskVoid LoadActiveSceneAsync(SceneReference sceneReference, CancellationToken cancellationToken,
            Action onSceneUnloaded = null)
        {
            if (CanLoadScene(sceneReference) == false)
                return;

            _isSceneLoading = true;

            await _screenCurtain.ShowAsync(cancellationToken).
                ContinueWith(() => UnloadActiveSceneAsync(onSceneUnloaded, cancellationToken)).
                ContinueWith(() => _adsService.TryShowInterstitialAdAsync(cancellationToken)).
                ContinueWith(() => SceneManager.LoadSceneAsync(sceneReference.BuildIndex, LoadSceneMode.Additive).
                    ToUniTask(_screenCurtain as IProgress<float>, cancellationToken: cancellationToken));

            SetActiveScene(sceneReference.BuildIndex);

            await UniTask.NextFrame(cancellationToken).
                ContinueWith(() => _screenCurtain.HideAsync(cancellationToken));

            _isSceneLoading = false;
        }

        public async UniTask LoadAdditionalSceneAsync(SceneReference sceneReference, CancellationToken cancellationToken)
        {
            if (CanLoadScene(sceneReference) == false)
                return;

            await SceneManager.LoadSceneAsync(sceneReference.BuildIndex, LoadSceneMode.Additive).
                ToUniTask(cancellationToken: cancellationToken);
        }

        private async UniTask UnloadActiveSceneAsync(Action onSceneUnloaded, CancellationToken cancellationToken)
        {
            Scene activeScene = SceneManager.GetActiveScene();

            await SceneManager.UnloadSceneAsync(activeScene.buildIndex).
                ToUniTask(cancellationToken: cancellationToken);

            onSceneUnloaded?.Invoke();
        }

        private void SetActiveScene(int buildIndex)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(buildIndex);
            SceneManager.SetActiveScene(scene);
        }

        private bool CanLoadScene(SceneReference sceneReference)
        {
            if (sceneReference.IsAddedInBuild == false)
                Logger.Log($"Scene <{sceneReference.Name}> was not added in build", LogMode.Error);

            return _isSceneLoading == false && sceneReference.IsAddedInBuild == true;
        }
    }
}
