using UnityEngine;
using Zenject;

namespace Project
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AllCellsOccupiedStrategy>().FromNew().AsSingle();
        }
    }
}
