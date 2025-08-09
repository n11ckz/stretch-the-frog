using UnityEngine;

namespace Project
{
    public static class GameObjectExtensions
    {
        public static void Enable(this GameObject gameObject) =>
            gameObject.SetActive(true);

        public static void Disable(this GameObject gameObject) =>
            gameObject.SetActive(false);
    }
}
