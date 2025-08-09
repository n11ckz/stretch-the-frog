using UnityEngine;
using Zenject;

namespace Project
{
    public class MenuSceneCleaner : MonoBehaviour
    {
        [SerializeField] private MenuDisplayView _menuDisplayView;
        [SerializeField] private RectTransform _buttonsHolder;
        
        private LevelCycle _levelCycle;

        [Inject]
        private void Construct(LevelCycle levelCycle) =>
            _levelCycle = levelCycle;

        private void Awake() =>
            _levelCycle.Disposed += Clean;

        private void OnDestroy() =>
            _levelCycle.Disposed -= Clean;

        private void Clean()
        {
            _buttonsHolder.gameObject.Enable();
            _menuDisplayView.Hide(true, destroyCancellationToken).
                Forget();
        }
    }
}
