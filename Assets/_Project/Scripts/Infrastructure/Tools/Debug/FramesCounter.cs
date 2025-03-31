using TMPro;
using UnityEngine;

namespace Project
{
    public class FramesCounter : MonoBehaviour
    {
        private const float Smoothness = 0.1f;
        
        [SerializeField] private TMP_Text _text;
        [SerializeField, Min(0.0f)] private float _delay;
        [SerializeField] private bool _shouldDisplayed;

        private float _elapsedTime;
        private float _deltaTime;

        private void Awake()
        {
            if (_shouldDisplayed == false)
                gameObject.Disable();
        }

        private void Update()
        {
            _elapsedTime += Time.unscaledDeltaTime;
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * Smoothness;

            if (_elapsedTime > _delay)
            {
                int frames = Mathf.CeilToInt(1.0f / _deltaTime);
                _text.text = frames.ToString();
                _elapsedTime = 0.0f;
            }
        }
    }
}
