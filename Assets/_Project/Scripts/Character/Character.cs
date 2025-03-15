using System;
using UnityEngine;
using Zenject;

namespace Project
{
    [SelectionBase]
    public class Character : MonoBehaviour, ICellOccupant
    {
        public event Action Stuck;

        public Transform Transform => transform;

        private IInput _input;
        private IMovement _movement;
        private ObstacleSensor _sensor;
        private CellMap _cellMap;

        [Inject]
        private void Construct(IInput input, ObstacleSensor sensor, CellMap cellMap)
        {
            _input = input;
            _sensor = sensor;
            _cellMap = cellMap;
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

            if (_sensor.IsStuck(transform.position) == true)
                Stuck?.Invoke();
        }
    }
}
