using DG.Tweening;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Configs/Character/" + nameof(CharacterConfig), fileName = nameof(CharacterConfig))]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField, Range(1.0f, 32.0f)] public float Speed { get; private set; }
        [field: SerializeField] public Ease MoveEase { get; private set; }
    }
}
