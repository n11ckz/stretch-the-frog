using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    public static class DirectionUtility
    {
        public static Vector3 ToVector(this Direction direction) => direction switch
        {
            Direction.Up => Vector3.forward,
            Direction.Down => Vector3.back,
            Direction.Left => Vector3.left,
            Direction.Right => Vector3.right,
            _ => Vector3.zero
        };

        public static IEnumerable<Direction> GetAllValidDirections() =>
            Enum.GetValues(typeof(Direction)).Cast<Direction>().Where((x) => x != Direction.None);
    }
}
