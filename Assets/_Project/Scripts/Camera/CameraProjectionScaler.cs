using Alchemy.Inspector;
using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(Camera))]
    public class CameraProjectionScaler : MonoBehaviour
    {
        private const float OrthographicSizeConverter = 0.5f;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LevelBorders _levelBorders;

        [SerializeField] private Vector3 _scopeOffset;

        private ScreenService _screenService;
        private Bounds _cachedBounds;

        [Inject]
        private void Construct(ScreenService screenService) =>
            _screenService = screenService;

        private void Awake() =>
            _screenService.ResolutionChanged += AdjustOrthographicSize;

        private void Start() =>
            Initialize();

        private void OnDestroy() =>
            _screenService.ResolutionChanged -= AdjustOrthographicSize;

        [Button]
        private void Initialize()
        {
            _cachedBounds = _levelBorders.CalculateBounds();
            _mainCamera.transform.position = _cachedBounds.center.
                With(y: _mainCamera.transform.position.y);
            AdjustOrthographicSize();
        }

        private void AdjustOrthographicSize()
        {
            Bounds expandedBounds = _cachedBounds.ExpandAndReturnCopy(_scopeOffset);
            float width = expandedBounds.size.x / _mainCamera.aspect;
            float height = expandedBounds.size.z;

            _mainCamera.orthographicSize = Mathf.Max(width, height) * OrthographicSizeConverter;
        }
    }
}
