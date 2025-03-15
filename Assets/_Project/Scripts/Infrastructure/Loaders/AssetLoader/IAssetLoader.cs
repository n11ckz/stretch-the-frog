using UnityEngine;

namespace Project
{
    public interface IAssetLoader
    {
        public T Load<T>(string path) where T : Object;
    }
}
