using UnityEngine;

namespace Project
{
    public static class TransformExtensions
    {
        public static void SetParentAndSiblingIndex(this Transform transform, Transform parent, int index)
        {
            transform.SetParent(parent);
            transform.SetSiblingIndex(index);
        }
    }
}
