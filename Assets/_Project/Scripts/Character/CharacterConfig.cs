using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/Character/" + nameof(CharacterConfig))]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField, Range(1.0f, 24.0f)] public float Speed { get; private set; }
    }
}
