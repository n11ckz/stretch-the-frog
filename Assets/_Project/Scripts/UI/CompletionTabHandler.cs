using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CompletionTabHandler : MonoBehaviour
    {
        [SerializeField] private ScrollMenu _scrollMenu;
        [SerializeField] private ScrollableTab _tab;

        private BetweenScenesMediator _betweenScenesMediator;
        private Action<bool> _cachedShowAction;
        private int _initialChildTabIndex;

        [Inject]
        private void Construct(BetweenScenesMediator betweenScenesMediator) =>
            _betweenScenesMediator = betweenScenesMediator;

        private void OnDestroy() =>
            _betweenScenesMediator.LevelCompleted -= _cachedShowAction;

        public void Initialize()
        {
            _cachedShowAction = (isSuccessfully) => ShowAsync(isSuccessfully).Forget();
            _betweenScenesMediator.LevelCompleted += _cachedShowAction;
            _initialChildTabIndex = _tab.transform.GetSiblingIndex();

            DisableTab();
        }

        public void DisableTab()
        {
            _scrollMenu.RemoveTab(_tab.Type, (tab) =>
            {
                tab.transform.SetParent(transform);
                tab.gameObject.Deactivate();
            });
        }

        private async UniTaskVoid ShowAsync(bool isSuccessfully)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.6f));

            _tab.gameObject.Activate();
            _scrollMenu.InsertTab(_initialChildTabIndex, _tab);
            _scrollMenu.OpenTab(_tab.Type);
        }
    }
}
