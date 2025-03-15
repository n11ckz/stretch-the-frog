using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Configs/Level/" + nameof(LevelSequence), fileName = nameof(LevelSequence))]
    public class LevelSequence : ScriptableObject
    {
        [SerializeField] private List<SceneReference> _levelSequence = new List<SceneReference>();

        public IReadOnlyList<SceneReference> LevelReferences => _levelSequence;
        public SceneReference InitialLevelReference => _levelSequence.First();
    }
}
