using Alchemy.Inspector;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private readonly int _blinkHash = Animator.StringToHash("Blink");
        
        [SerializeField] private Vector2 _delayBetweenAnimation;

        private Animator _animator;
        private float _currentDelay;
        private float _elapsedTime;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            ResetTimer();
        }

        private void Update() =>
            UpdateTimer();

        [Button]
        public void PlayBlinkAnimation()
        {
            _animator.SetTrigger(_blinkHash);
            ResetTimer();
        }

        private void UpdateTimer()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _currentDelay)
                PlayBlinkAnimation();
        }

        private void ResetTimer()
        {
            _currentDelay = Random.Range(_delayBetweenAnimation.x, _delayBetweenAnimation.y);
            _elapsedTime = 0.0f;
        }
    }
}
