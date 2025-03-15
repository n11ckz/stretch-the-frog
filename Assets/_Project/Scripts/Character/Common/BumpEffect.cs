using DG.Tweening;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(CharacterAnimator))]
    public class BumpEffect : MonoBehaviour
    {
        [SerializeField, Min(0.0f)] private float _punchStrength;
        [SerializeField, Range(0.0f, 0.5f)] private float _punchDuration;
        [SerializeField] private ParticleSystem _bumpParticle;
        
        private CharacterAnimator _animator;
        private Camera _camera;
        private Tween _tween;

        private void Awake()
        {
            _animator = GetComponent<CharacterAnimator>();
            _camera = Camera.main;
        }

        public void PlayEffect(Vector3 direction)
        {
            _tween?.Complete();
            _tween = _camera.transform.DOPunchPosition(direction * _punchStrength, _punchDuration);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction) * _bumpParticle.transform.rotation;
            Instantiate(_bumpParticle, transform.position.With(y: _bumpParticle.transform.position.y), rotation);
        }

        public void PlaySlimEffect(Vector3 direction)
        {
            _tween?.Complete();
            _tween = _camera.transform.DOPunchPosition(direction * _punchStrength, _punchDuration);
            _animator.PlayBlinkAnimation();
        }
    }
}
