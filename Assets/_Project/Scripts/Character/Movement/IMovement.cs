using System;
using UnityEngine;

namespace Project
{
    public interface IMovement
    {
        public event Action<Vector3> MovedAt;

        public DirectionInfo DirectionInfo { get; }

        public void Move(Direction direction);
        public void StopMove();
    }
}
