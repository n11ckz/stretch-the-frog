using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class CompletionPanel : MonoBehaviour
    {
        [SerializeField] private MenuDisplay _menuDisplay;
        [SerializeField] private LevelSelectionButtonsHolder _selectionButtonsHolder;

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

            _selectionButtonsHolder.gameObject.Deactivate();
            gameObject.Activate();

            _menuDisplay.Show().Forget();
        }
    }
}
