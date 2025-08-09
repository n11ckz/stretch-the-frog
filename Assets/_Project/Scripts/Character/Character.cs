using System;
using UnityEngine;
using Zenject;

namespace Project
{
    public class Character : MonoBehaviour, ICellOccupant
    {
        public event Action Stuck;

        public Transform Transform => transform;

        private IMovement _movement;
        private IInput _input;
        private CellMap _cellMap;
        private ObstacleSensor _obstacleSensor;

        [Inject]
        private void Construct(IInput input, CellMap cellMap, ObstacleSensor obstacleSensor)
        {
            _input = input;
            _cellMap = cellMap;
            _obstacleSensor = obstacleSensor;
        }

        private void Awake()
        {
            _movement = GetComponent<IMovement>();

            _input.DirectionReceived += Move;
            _movement.MovedAt += OccupyCell;
        }

        private void OnDestroy()
        {
            _input.DirectionReceived -= Move;
            _movement.MovedAt -= OccupyCell;
        }

        private void Move(Direction direction) =>
            _movement.Move(direction);

        private void OccupyCell(Vector3 worldPosition)
        {
            _cellMap.OccupyCellAt(worldPosition, this);

            if (_obstacleSensor.IsStuck(Transform.position))
                Stuck?.Invoke();
        }
    }
}
