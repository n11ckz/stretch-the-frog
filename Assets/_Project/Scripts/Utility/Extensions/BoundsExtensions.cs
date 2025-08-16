using UnityEngine;

namespace Project
{
    public static class BoundsExtensions
    {
        public static Bounds ExpandAndReturnCopy(this Bounds bounds, Vector3 amount)
        {
            bounds.Expand(amount);
            return bounds;
        }
    }
}
