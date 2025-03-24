using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class SceneLoaderFacade : MonoBehaviour
    {
        [SerializeField] private CompletionTabHandler _completionTabHandler;
        [SerializeField] private ScrollMenu _scrollMenu;
        
        public int ActiveSceneBuildIndex => _sceneLoader.ActiveScene.BuildIndex;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        public void ReloadActiveScene() =>
            _sceneLoader.ReloadActiveSceneAsync(() => HideAllElements()).Forget();

        public void LoadActiveScene(SceneReference scene) =>
            _sceneLoader.LoadActiveSceneAsync(scene, () => HideAllElements()).Forget();

        private void HideAllElements()
        {
            _completionTabHandler.DisableTab();
            _scrollMenu.Hide(true);
        }
    }
}
