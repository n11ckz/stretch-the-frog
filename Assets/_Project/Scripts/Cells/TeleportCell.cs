using UnityEngine;
using Zenject;

namespace Project
{
    public class TeleportCell : BaseCell
    {
        [SerializeField] private TeleportCell _anotherCell;

        private TraceFactory _traceFactory;
        private bool _isDeactivated;

        [Inject]
        private void Construct(TraceFactory traceFactory) =>
            _traceFactory = traceFactory;

        public override void Occupy(ICellOccupant occupant)
        {
            base.Occupy(occupant);

            if (_isDeactivated == true)
                return;

            _anotherCell.Deactivate();
            _anotherCell.Occupy(occupant);

            occupant.Transform.position = _anotherCell.transform.position;

            LeaveTraceOnCell(occupant);
        }

        public void Deactivate() =>
            _isDeactivated = true;

        private void LeaveTraceOnCell(ICellOccupant occupant)
        {
            if (occupant.Transform.TryGetComponent(out IMovement movement) == false)
                return;

            DirectionInfo directionInfo = new DirectionInfo() { Current = movement.CurrentDirection };
            Trace trace = _traceFactory.CreateTrace(directionInfo);

            trace.transform.position = transform.position.With(y: trace.PositionOffset.y);
        }
    }
}
