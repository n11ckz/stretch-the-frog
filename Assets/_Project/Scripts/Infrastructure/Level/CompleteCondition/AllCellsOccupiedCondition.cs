using System;
using Zenject;

namespace Project
{
    public class AllCellsOccupiedCondition : ICompleteCondition, IInitializable, IDisposable
    {
        public event Action<bool> Completed;

        private readonly CellMap _cellMap;
        private readonly Character _character;

        public AllCellsOccupiedCondition(CellMap cellMap, Character character)
        {
            _cellMap = cellMap;
            _character = character;
        }

        public void Initialize() =>
            _character.Stuck += Complete;

        public void Dispose() =>
            _character.Stuck -= Complete;

        private void Complete()
        {
            bool isSuccessfully = _cellMap.TotalCellCount == _cellMap.OccupiedCells.Count;
            Completed?.Invoke(isSuccessfully);
        }
    }
}
