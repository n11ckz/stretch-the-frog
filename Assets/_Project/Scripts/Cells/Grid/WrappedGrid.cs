using UnityEngine;

namespace Project
{
    public class WrappedGrid : IGrid
    {
        private readonly Grid _grid;

        public WrappedGrid(Grid grid) =>
            _grid = grid;

        public Vector3Int ToCellPosition(Vector3 worldPosition) =>
            _grid.WorldToCell(worldPosition);
    }
}
