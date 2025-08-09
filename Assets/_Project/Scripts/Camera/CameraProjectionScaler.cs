using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(Camera))]
    public class CameraProjectionScaler : MonoBehaviour
    {
        private const float Offset = 0.001f;

        [SerializeField] private Camera _camera;

        [SerializeField] private OrthographicSizeData _landscapeData;
        [SerializeField] private OrthographicSizeData _portraitData;

        private ScreenService _screenService;

        [Inject]
        private void Construct(ScreenService screenService) =>
            _screenService = screenService;

        private void Awake() =>
            _screenService.ResolutionChanged += Adjust;

        private void Start() =>
            Adjust();

        private void OnDestroy() =>
            _screenService.ResolutionChanged -= Adjust;

        private void Adjust()
        {
            OrthographicSizeData data = _screenService.IsLandscapeResolution == true ? _landscapeData : _portraitData;
            float orthographicSize = Mathf.Lerp(data.Width / _camera.aspect, data.Size, data.Match) + Offset;
            _camera.orthographicSize = orthographicSize;
        }
    }
}
