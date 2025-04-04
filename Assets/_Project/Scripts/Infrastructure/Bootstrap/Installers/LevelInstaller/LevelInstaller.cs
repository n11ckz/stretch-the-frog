using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(CharacterInstaller), typeof(CellsInstaller))]
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ObstacleSensor>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<Level>().FromNew().AsSingle();
            Container.BindInterfacesTo<AllCellsOccupiedCondition>().FromNew().AsSingle();
        }
    }
}
