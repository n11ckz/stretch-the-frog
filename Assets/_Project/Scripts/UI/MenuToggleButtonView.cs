using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project
{
    public class MenuToggleButtonView : MonoBehaviour
    {
        [SerializeField] private Button _toggleButton;
        [SerializeField] private MenuDisplayView _displayView;

        private PauseService _pauseService;

        [Inject]
        private void Construct(PauseService pauseService) =>
            _pauseService = pauseService;

        private void Awake() =>
            _toggleButton.onClick.AddListener(Toggle);

        private void OnDestroy() =>
            _toggleButton.onClick.RemoveListener(Toggle);

        private void Toggle()
        {
            if (_pauseService.IsPaused == false)
            {
                _pauseService.Pause();
                _displayView.Show(destroyCancellationToken).Forget();
            }
            else
            {
                _pauseService.Resume();
                _displayView.Hide(false, destroyCancellationToken).Forget();
            }
        }
    }
}
