using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ReloadSceneButton : BaseQuickAccessButton
    {
        [SerializeField] private ScrollableMenu _scrollableMenu;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        protected override void Execute() =>
            _sceneLoader.ReloadActiveSceneAsync(() => HideMenu()).Forget();

        private void HideMenu()
        {
            if (_scrollableMenu.gameObject.activeInHierarchy == false)
                return;

            _scrollableMenu.Hide(true);
        }
    }
}
