using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ReloadActiveSceneButton : BaseQuickAccessButton
    {
        [SerializeField] private MenuResetter _menuResetter;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Contruct(SceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        protected override void Execute() =>
            _sceneLoader.ReloadActiveSceneAsync(() => _menuResetter.ResetElements()).Forget();
    }
}
