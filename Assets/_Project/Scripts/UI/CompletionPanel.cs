using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CompletionPanel : MonoBehaviour
    {
        [SerializeField] private ScrollableMenu _scrollableMenu;

        private BetweenScenesMediator _betweenScenesMediator;
        private Action<bool> _cachedShowAction;

        [Inject]
        private void Construct(BetweenScenesMediator betweenScenesMediator)
        {
            _betweenScenesMediator = betweenScenesMediator;
        }

        private void Awake()
        {
            _cachedShowAction = (isSuccessfully) => ShowAsync(isSuccessfully).Forget();
            _betweenScenesMediator.LevelCompleted += _cachedShowAction;

            gameObject.Deactivate();
        }

        private void OnDestroy()
        {
            _betweenScenesMediator.LevelCompleted -= _cachedShowAction;
        }

        private async UniTaskVoid ShowAsync(bool isSuccessfully)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            _scrollableMenu.OpenTab(ScrollableTabType.Tab2);
        }
    }
}
