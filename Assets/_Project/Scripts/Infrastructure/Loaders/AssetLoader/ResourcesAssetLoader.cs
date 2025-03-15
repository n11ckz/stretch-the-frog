using UnityEngine;

namespace Project
{
    public class ResourcesAssetLoader : IAssetLoader
    {
        public T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);
    }
}
