using Alchemy.Inspector;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private readonly int _blinkHash = Animator.StringToHash("Blink");

        [SerializeField] private Animator _animator;

        [Button]
        public void PlayBlinkAnimation() =>
            _animator.SetTrigger(_blinkHash);
    }
}
