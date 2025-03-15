using UnityEngine;
using Zenject;

namespace Project
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CharacterInitialPoint _characterInitialPoint;
        [SerializeField] private CharacterConfig _characterConfig;

        public override void InstallBindings()
        {
            Container.Bind<Character>().FromComponentInNewPrefab(_characterPrefab).AsSingle().NonLazy();

            Container.Bind<CharacterInitialPoint>().FromInstance(_characterInitialPoint).AsSingle();
            Container.Bind<CharacterConfig>().FromInstance(_characterConfig).AsSingle();
        }
    }
}
