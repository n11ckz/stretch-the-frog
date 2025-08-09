using UnityEngine;
using Zenject;

namespace Project
{
    public class LevelBootstrap : MonoBehaviour
    {
        private ILevelCompletionStrategy _completionStrategy;
        private LevelCycle _levelCycle;
        private CellMap _cellMap;
        private Character _character;
        private CharacterInitialPoint _initialPoint;

        [Inject]
        private void Construct(ILevelCompletionStrategy completionStrategy, LevelCycle levelCycle,
            CellMap cellMap, Character character, CharacterInitialPoint initialPoint)
        {
            _completionStrategy = completionStrategy;
            _levelCycle = levelCycle;
            _cellMap = cellMap;
            _character = character;
            _initialPoint = initialPoint;
        }

        private void Start()
        {
            _cellMap.RegisterCells();
            _character.Transform.position = _initialPoint.Position;
            _levelCycle.Start(_completionStrategy, destroyCancellationToken).Forget();
        }
    }
}
