using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LevelSequence _levelSequence;
        [SerializeField] private FadingScreenCurtain _screenCurtainPrefab;

        public override void InstallBindings()
        {
            Type inputType = Application.isMobilePlatform == false ? typeof(KeyboardInput) : typeof(TouchscreenInput);
            Container.BindInterfacesTo(inputType).FromNew().AsSingle();

            Container.Bind<IScreenCurtain>().FromComponentInNewPrefab(_screenCurtainPrefab).AsSingle();

            Container.BindInterfacesTo<FastReloadShortcut>().FromNew().AsSingle().NonLazy();

            Container.Bind<SceneLoader>().FromNew().AsSingle();
            Container.Bind<LevelLoader>().FromNew().AsSingle();
            Container.Bind<LevelCycle>().FromNew().AsSingle();
            Container.Bind<PauseService>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<ScreenService>().FromNew().AsSingle();

            Container.Bind<LevelSequence>().FromInstance(_levelSequence).AsSingle();

            Container.Bind<IAdsService>().To<NoAdsService>().FromNew().AsSingle();

            Container.Bind<ILogService>().To<UnityLogService>().FromNew().AsSingle().
                OnInstantiated<ILogService>((context, x) => Logger.Initialize(x)).NonLazy();
        }
    }
}
