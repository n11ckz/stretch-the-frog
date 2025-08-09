using DG.Tweening;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(CharacterAnimator))]
    public class BumpEffect : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private ParticleSystem _bumpVfx;
        [SerializeField] private Transform _vfxPoint;

        [SerializeField, Min(0.0f)] private float _punchStrength;
        [SerializeField, Range(0.0f, 0.5f)] private float _punchDuration;

        private Transform _camera;
        private Tween _tween;

        private void Awake() =>
            _camera = Camera.main.transform;

        public void PlayEffect(Vector3 direction)
        {
            PunchCameraAlongDirection(direction);

            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
            Instantiate(_bumpVfx, _vfxPoint.position, rotation * _bumpVfx.transform.rotation);
        }

        public void PlaySlimEffect(Vector3 direction)
        {
            PunchCameraAlongDirection(direction);
            _animator.PlayBlinkAnimation();
        }

        private void PunchCameraAlongDirection(Vector3 direction)
        {
            _tween?.Complete();

            Vector3 punch = direction * _punchStrength;
            _tween = _camera.DOPunchPosition(punch, _punchDuration);
        }
    }
}
