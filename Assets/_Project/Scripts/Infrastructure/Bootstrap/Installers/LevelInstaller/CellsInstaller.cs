using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(LevelInstaller))]
    public class CellsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cellParent;
        [SerializeField] private Grid _grid;

        public override void InstallBindings()
        {
            Container.Bind<CellMap>().FromNew().AsSingle().WithArguments(_cellParent);
            Container.Bind<IGrid>().To<WrappedGrid>().FromNew().AsSingle().WithArguments(_grid);
        }
    }
}
