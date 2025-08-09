using System;
using UnityEngine;

namespace Project
{
    [Serializable]
    public class OrthographicSizeData
    {
        [field: SerializeField, Min(0.1f)] public float Size { get; private set; }
        [field: SerializeField, Range(0.0f, 1.0f)] public float Match { get; private set; }
        [field: SerializeField] public Vector2 AspectRatio { get; private set; }

        public float Width => Size * (AspectRatio.x / AspectRatio.y);
    }
}
