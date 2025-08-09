namespace Project
{
    public class CrossroadCell : BaseCell
    {
        public override void Occupy(ICellOccupant cellOccupant)
        {
            base.Occupy(cellOccupant);

            if (cellOccupant.Transform.TryGetComponent(out IMovement movement) == false)
                return;

            movement.StopMove();
        }
    }
}
