using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class CellMap
    {
        public event Action CellOccupied;
        
        private readonly Dictionary<Vector3Int, BaseCell> _map = new Dictionary<Vector3Int, BaseCell>();
        private readonly List<BaseCell> _occupiedCells = new List<BaseCell>();

        private readonly IGrid _grid;
        private readonly ILogger _logger;
        private readonly Transform _parent;

        public int TotalCellCount => _map.Count;
        public IReadOnlyList<BaseCell> OccupiedCells => _occupiedCells;

        public CellMap(IGrid grid, ILogger logger, Transform parent)
        {
            _grid = grid;
            _logger = logger;
            _parent = parent;
        }

        public void Initialize()
        {
            BaseCell[] cells = _parent.GetComponentsInChildren<BaseCell>();

            foreach (BaseCell cell in cells)
            {
                Vector3Int cellGridPosition = _grid.ToCellPosition(cell.transform.position);

                if (_map.TryAdd(cellGridPosition, cell) == false)
                {
                    _logger.Log($"Duplicate! Position of <{cell.name}> has already been added");
                    continue;
                }

                // It's not overengineering! This logic needed to fix the problem with <TeleportCell> for correct display <ProgressBar>
                cell.Occupied += RegisterOccupiedCell; 
            }
        }

        public void OccupyCellAt(Vector3 worldPosition, ICellOccupant occupant)
        {
            Vector3Int cellPosition = _grid.ToCellPosition(worldPosition);

            if (IsValidCellToOccupy(cellPosition, out BaseCell cell) == false)
                return;

            cell.Occupy(occupant);
            CellOccupied?.Invoke();
        }

        private void RegisterOccupiedCell(BaseCell cell)
        {
            cell.Occupied -= RegisterOccupiedCell;
            _occupiedCells.Add(cell);
        }

        private bool IsValidCellToOccupy(Vector3Int cellPosition, out BaseCell cell) =>
            _map.TryGetValue(cellPosition, out cell) == true && cell.IsOccupied == false;
    }
}
