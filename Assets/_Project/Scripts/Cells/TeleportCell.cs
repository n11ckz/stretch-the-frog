using UnityEngine;
using Zenject;

namespace Project
{
    public class TeleportCell : BaseCell
    {
        [SerializeField] private TeleportCell _anotherCell;

        private TraceFactory _traceFactory;

        [Inject]
        private void Construct(TraceFactory traceFactory)
        {
            _traceFactory = traceFactory;
            // TODO: sound player
        }

        public override void Occupy(ICellOccupant cellOccupant)
        {
            base.Occupy(cellOccupant);

            if (enabled == false)
                return;

            _anotherCell.enabled = false;
            _anotherCell.Occupy(cellOccupant);

            LeaveTrace(cellOccupant);

            cellOccupant.Transform.position = _anotherCell.transform.position;
        }

        private void LeaveTrace(ICellOccupant cellOccupant)
        {
            if (cellOccupant.Transform.TryGetComponent(out IMovement movement) == false)
                return;

            Trace trace = _traceFactory.CreateTrace(movement.DirectionInfo);
            trace.transform.position = cellOccupant.Transform.position;
        }
    }
}
