using UnityEngine;
using Zenject;

namespace Project
{
    public class CellMapInstaller : MonoInstaller
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Transform _cellContainer;

        public override void InstallBindings()
        {
            Container.Bind<IGrid>().To<BuildInGrid>().FromNew().AsSingle().WithArguments(_grid);
            Container.Bind<CellMap>().FromNew().AsSingle().WithArguments(_cellContainer);
        }
    }
}
