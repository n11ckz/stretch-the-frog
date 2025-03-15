using UnityEngine;
using Zenject;

namespace Project
{
    public static class IInstantiatorExtensions
    {
        public static T Instantiate<T>(this IInstantiator instantiator, T prefab) where T : Object =>
            instantiator.InstantiatePrefabForComponent<T>(prefab);
    }
}
