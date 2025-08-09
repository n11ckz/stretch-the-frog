using Cysharp.Threading.Tasks;
using System.Threading;

namespace Project
{
    public class AllCellsOccupiedStrategy : ILevelCompletionStrategy
    {
        private readonly Character _character;
        private readonly CellMap _cellMap;

        public AllCellsOccupiedStrategy(Character character, CellMap cellMap)
        {
            _character = character;
            _cellMap = cellMap;
        }

        public UniTask<bool> WaitForCompletionAsync(CancellationToken cancellationToken)
        {
            UniTaskCompletionSource<bool> source = new UniTaskCompletionSource<bool>();
            cancellationToken.Register(() => SetCanceled());

            void SetCanceled()
            {
                source.TrySetCanceled(cancellationToken);
                _character.Stuck -= SetResult;
            }

            void SetResult()
            {
                bool isSuccessfully = _cellMap.OccupiedCellCount == _cellMap.TotalCellCount;
                source.TrySetResult(isSuccessfully);
                _character.Stuck -= SetResult;
            }

            _character.Stuck += SetResult;

            return source.Task;
        }
    }
}
