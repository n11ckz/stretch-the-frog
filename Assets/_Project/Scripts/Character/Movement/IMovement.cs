using System;
using UnityEngine;

namespace Project
{
    public interface IMovement
    {
        public event Action<Vector3> MovedAt;

        public Direction CurrentDirection { get; }

        public void Move(Direction direction);
        public void StopMove();
    }
}
