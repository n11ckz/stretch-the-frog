using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    public class ObstacleSensor
    {
        private const float MaxRayDistance = 1.0f;

        private readonly IReadOnlyList<Vector3> _directions;

        public ObstacleSensor() =>
            _directions = DirectionUtils.GetAllDirectionsFromEnum().
                Where((direction) => direction != Direction.None).Select((direction) => direction.ToVector()).ToList();

        public bool HasObstacleAhead(Vector3 origin, Vector3 direction) =>
            Physics.Raycast(origin, direction, MaxRayDistance) == true;

        public bool IsStuck(Vector3 origin) =>
            _directions.All((direction) => HasObstacleAhead(origin, direction)) == true;
    }
}
