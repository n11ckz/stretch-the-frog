namespace Project
{
    public class CrossroadCell : BaseCell
    {
        public override void Occupy(ICellOccupant occupant)
        {
            base.Occupy(occupant);
            StopOccupantMovement(occupant);
        }

        private void StopOccupantMovement(ICellOccupant occupant)
        {
            if (occupant.Transform.TryGetComponent(out IMovement movement) == false)
                return;

            movement.StopMove();
        }
    }
}
