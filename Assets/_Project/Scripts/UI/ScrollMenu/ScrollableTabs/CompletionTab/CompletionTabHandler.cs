using Coffee.UIExtensions;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CompletionTabHandler : MonoBehaviour
    {
        [SerializeField] private ScrollMenu _scrollMenu;
        [SerializeField] private CompletionTab _tab;
        [SerializeField] private UIParticle _confettiVfx;

        private BetweenScenesMediator _betweenScenesMediator;
        private Action<bool> _cachedShowAction;
        private int _childTabIndex;

        [Inject]
        private void Construct(BetweenScenesMediator betweenScenesMediator) =>
            _betweenScenesMediator = betweenScenesMediator;

        private void OnDestroy() =>
            _betweenScenesMediator.LevelCompleted -= _cachedShowAction;

        public void Initialize()
        {
            _cachedShowAction = (isSuccessfully) => ShowAsync(isSuccessfully).Forget();
            _betweenScenesMediator.LevelCompleted += _cachedShowAction;
            _childTabIndex = _tab.transform.GetSiblingIndex();

            DisableTab();
        }

        public void DisableTab()
        {
            _confettiVfx.Stop();
            _scrollMenu.RemoveTab(_tab.Type, (tab) =>
            {
                tab.transform.SetParent(transform);
                tab.gameObject.Disable();
            });
        }

        private async UniTaskVoid ShowAsync(bool isSuccessfully)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.6f));

            _tab.EnableSetup(isSuccessfully);
            _scrollMenu.InsertTab(_childTabIndex, _tab);
            _scrollMenu.OpenTab(_tab.Type);
            _confettiVfx.Play();
        }
    }
}
