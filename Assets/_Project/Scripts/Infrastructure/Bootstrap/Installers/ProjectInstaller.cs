using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ScreenCurtain _screenCurtain;
        [SerializeField] private LevelSequence _levelSequence;

        public override void InstallBindings()
        {
            Type inputType = Application.isMobilePlatform == true ? typeof(TouchscreenInput) : typeof(KeyboardInput);
            Container.BindInterfacesTo(inputType).FromNew().AsSingle();

            Container.Bind<ILogger>().To<ConsoleLogger>().FromNew().AsSingle();
            Container.Bind<IAssetLoader>().To<ResourcesAssetLoader>().FromNew().AsSingle();

            Container.Bind<SceneLoader>().FromNew().AsSingle();
            Container.Bind<PauseHandler>().FromNew().AsSingle();
            Container.Bind<BetweenScenesMediator>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<TraceFactory>().FromNew().AsSingle();

            Container.Bind<ScreenCurtain>().FromComponentInNewPrefab(_screenCurtain).AsSingle();
            Container.Bind<LevelSequence>().FromInstance(_levelSequence).AsSingle();

            // TODO: remove it later
            Container.BindInterfacesAndSelfTo<LevelSelectionButtonFactory>().FromNew().AsSingle();
            Container.Bind<ProgressStorage>().FromNew().AsSingle();
        }
    }
}
