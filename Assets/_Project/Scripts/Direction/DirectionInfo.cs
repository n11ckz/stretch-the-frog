using System;
using UnityEngine;

namespace Project
{
    [Serializable]
    public struct DirectionInfo
    {
        [field: SerializeField] public Direction Current { get; set; }
        [field: SerializeField] public Direction Previous { get; set; }
    }
}
