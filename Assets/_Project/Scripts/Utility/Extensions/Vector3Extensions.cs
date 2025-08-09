using UnityEngine;

namespace Project
{
    public static class Vector3Extensions
    {
        public static Vector3 Add(this Vector3 vector, float x = 0.0f, float y = 0.0f, float z = 0.0f) =>
            new Vector3(vector.x + x, vector.y + y, vector.z + z);

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) =>
            new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
}
