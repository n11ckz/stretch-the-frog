using System;
using Zenject;

namespace Project
{
    public class AllCellsOccupiedCondition : ICompletionCondition, IInitializable, IDisposable
    {
        public event Action<bool> Checked;

        private readonly CellMap _cellMap;
        private readonly Character _character;

        public AllCellsOccupiedCondition(CellMap cellMap, Character character)
        {
            _cellMap = cellMap;
            _character = character;
        }

        public void Initialize() =>
            _character.Stuck += Check;

        public void Dispose() =>
            _character.Stuck -= Check;

        private void Check()
        {
            bool isSuccessfully = _cellMap.TotalCellCount == _cellMap.OccupiedCells.Count;
            Checked?.Invoke(isSuccessfully);
        }
    }
}
