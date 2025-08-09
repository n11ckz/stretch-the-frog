#pragma warning disable UNT0006

using Cysharp.Threading.Tasks;
using n11ckz.SceneReference;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ProjectBootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference _overlayMenuScene;
        
        private SceneLoader _sceneLoader;
        private LevelLoader _levelLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader, LevelLoader levelLoader)
        {
            _sceneLoader = sceneLoader;
            _levelLoader = levelLoader;
        }

        private async UniTaskVoid Start()
        {
            await _sceneLoader.LoadAdditionalSceneAsync(_overlayMenuScene, Application.exitCancellationToken);

            _levelLoader.LoadNextLevelInSequence();
        }
    }
}
