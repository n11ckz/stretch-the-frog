using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace Project
{
    public static class DirectionUtils
    {
        public static Vector3 ToVector(this Direction direction) => direction switch
        {
            Direction.Up => Vector3.forward,
            Direction.Down => Vector3.back,
            Direction.Left => Vector3.left,
            Direction.Right => Vector3.right,
            _ => Vector3.zero
        };

        public static IEnumerable<Direction> GetAllDirectionsFromEnum() =>
            Enum.GetValues(typeof(Direction)).Cast<Direction>();
    }
}
