using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ProjectBootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference _menuScene;

        private SceneLoader _sceneLoader;
        private LevelSequence _levelSequence;
        private SavedProgressStorage _progressStorage;

        [Inject]
        private void Construct(SceneLoader sceneLoader, LevelSequence levelSequence, SavedProgressStorage progressStorage)
        {
            _sceneLoader = sceneLoader;
            _levelSequence = levelSequence;
            _progressStorage = progressStorage;
        }

        private async UniTaskVoid Start()
        {
            // Enable YG2 Plugin
            
            await _sceneLoader.LoadAdditionalAsync(_menuScene).
                ContinueWith(() => UniTask.WaitForEndOfFrame(this));

            // Load saves if they exists
            // Load last active level else load first level

            _progressStorage.Load();

            _sceneLoader.LoadActiveSceneAsync(_levelSequence.InitialLevelReference).Forget();
        }
    }
}
