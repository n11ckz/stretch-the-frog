using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Configs/Level/" + nameof(LevelSelectionButtonConfig), fileName = nameof(LevelSelectionButtonConfig))]
    public class LevelSelectionButtonConfig : ScriptableObject
    {
        [field: SerializeField] public LevelSelectionButton ButtonPrefab { get; private set; }
        [field: SerializeField] public Color LockedBackgroundColor { get; private set; }
        [field: SerializeField] public Color UnlockedBackgroundColor { get; private set; }
        [field: SerializeField] public Color FollowedBackgroundColor { get; private set; }
    }
}
