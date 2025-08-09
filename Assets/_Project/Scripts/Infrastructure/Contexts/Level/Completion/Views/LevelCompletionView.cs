using UnityEngine;
using Zenject;

namespace Project
{
    public class LevelCompletionView : MonoBehaviour
    {
        [SerializeField] private MenuDisplayView _menuDisplayView;
        
        private LevelCycle _levelCycle;

        [Inject]
        private void Construct(LevelCycle levelCycle) =>
            _levelCycle = levelCycle;

        private void Awake() =>
            _levelCycle.Completed += Show;

        private void OnDestroy() =>
            _levelCycle.Completed -= Show;

        private void Show(bool isSuccessfully)
        {
            _menuDisplayView.Show(destroyCancellationToken).Forget();
        }
    }
}
