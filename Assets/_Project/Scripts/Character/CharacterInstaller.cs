using UnityEngine;
using Zenject;

namespace Project
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private CharacterInitialPoint _characterInitialPoint;
        [SerializeField] private CharacterConfig _characterConfig;

        [SerializeField] private TraceConfig _traceConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<Character>().FromComponentInNewPrefab(_characterPrefab).AsSingle().NonLazy();

            Container.Bind<CharacterInitialPoint>().FromInstance(_characterInitialPoint).AsSingle();
            Container.Bind<CharacterConfig>().FromInstance(_characterConfig).AsSingle();

            Container.Bind<ObstacleSensor>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<TraceFactory>().FromNew().AsSingle().WithArguments(_traceConfig);
        }
    }
}
