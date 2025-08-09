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
            _directions = DirectionUtility.GetAllValidDirections().Select((x) => x.ToVector()).ToList();

        public bool HasObstacleAhead(Vector3 origin, Vector3 direction)
        {
            Ray ray = new Ray(origin, direction);
            return Physics.Raycast(ray, MaxRayDistance) == true;
        }

        public bool IsStuck(Vector3 origin) =>
            _directions.All((x) => HasObstacleAhead(origin, x)) == true;
    }
}
