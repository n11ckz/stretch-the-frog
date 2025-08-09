using UnityEngine;

namespace Project
{
    public class BuildInGrid : IGrid
    {
        private readonly Grid _grid;

        public BuildInGrid(Grid grid) =>
            _grid = grid;

        public Vector3Int ToCellPosition(Vector3 worldPosition) =>
            _grid.WorldToCell(worldPosition);
    }
}
