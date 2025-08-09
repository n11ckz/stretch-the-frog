using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(CharacterAnimator))]
    public class TimedAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private Vector2 _delayRange;

        private float _delay;
        private float _elapsedTime;

        private void Awake() =>
            ResetTime();

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _delay)
            {
                _animator.PlayBlinkAnimation();
                ResetTime();
            }
        }

        private void ResetTime()
        {
            _delay = Random.Range(_delayRange.x, _delayRange.y);
            _elapsedTime = 0.0f;
        }
    }
}
