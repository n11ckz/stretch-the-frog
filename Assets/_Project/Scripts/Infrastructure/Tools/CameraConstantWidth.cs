using Alchemy.Inspector;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Camera))]
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Vector2 _targetAspect;

        private Camera _camera;
        private float _initialSize;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _initialSize = _camera.orthographicSize;

            AdjustWidth();
        }

        [Button]
        private void AdjustWidth()
        {
            float aspect = _targetAspect.x / _targetAspect.y;
            float constantWidth = _initialSize * aspect;

            _camera.orthographicSize = constantWidth / _camera.aspect;
        }
    }
}
