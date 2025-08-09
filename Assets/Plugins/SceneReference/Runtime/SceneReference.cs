using System;
using UnityEngine;

namespace n11ckz.SceneReference
{
    [Serializable]
    public partial class SceneReference
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int BuildIndex { get; private set; }

        public bool IsAddedInBuild => BuildIndex != Constants.InvalidSceneBuildIndex;

        public SceneReference(string name, int buildIndex)
        {
            Name = name;
            BuildIndex = buildIndex;
        }
    }
}
