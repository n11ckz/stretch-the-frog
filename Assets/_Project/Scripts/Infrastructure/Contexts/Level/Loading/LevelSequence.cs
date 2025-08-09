using n11ckz.SceneReference;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(LevelSequence), menuName = "Configs/Level/" + nameof(LevelSequence))]
    public class LevelSequence : ScriptableObject
    {
        [SerializeField] private List<SceneReference> _levels;

        public IReadOnlyList<SceneReference> Levels => _levels;
        public SceneReference this[int index] => _levels[index];
    }
}
