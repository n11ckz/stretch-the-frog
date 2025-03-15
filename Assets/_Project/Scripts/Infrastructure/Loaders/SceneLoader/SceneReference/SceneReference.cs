using Alchemy.Inspector;
using System;
using UnityEngine;

namespace Project
{
    [Serializable]
    public partial class SceneReference
    {
        public const int InvalidSceneBuildIndex = -1;

        [field: SerializeField, ReadOnly] public string Name { get; private set; }
        [field: SerializeField, ReadOnly] public int BuildIndex { get; private set; }
    }
}
