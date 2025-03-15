using UnityEngine;
using Zenject;

namespace Project
{
    public class LevelBootstrap : MonoBehaviour
    {
        private Level _level;
        private CellMap _cellMap;
        private Character _character;
        private CharacterInitialPoint _initialPoint;

        [Inject]
        private void Construct(Level level, CellMap cellMap, Character character, CharacterInitialPoint initialPoint)
        {
            _level = level;
            _cellMap = cellMap;
            _character = character;
            _initialPoint = initialPoint;
        }

        private void Start()
        {
            _character.Transform.position = _initialPoint.Position;
            _cellMap.Initialize();
            _level.Start();
        }
    }
}
