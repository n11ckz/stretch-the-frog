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
        private readonly Transform _container;

        public int TotalCellCount => _map.Count;
        public int OccupiedCellCount => _occupiedCells.Count;

        public CellMap(IGrid grid, Transform container)
        {
            _grid = grid;
            _container = container;
        }

        public void RegisterCells()
        {
            BaseCell[] cells = _container.GetComponentsInChildren<BaseCell>();

            foreach (BaseCell cell in cells)
            {
                Vector3Int cellGridPosition = _grid.ToCellPosition(cell.transform.position);

                if (_map.TryAdd(cellGridPosition, cell) == false)
                {
                    Logger.Log($"Duplicate! Position of <{cell.name}> has already been added", LogMode.Warning);
                    continue;
                }

                cell.Occupied += RegisterOccupiedCell;
            }
        }

        public void OccupyCellAt(Vector3 worldPosition, ICellOccupant cellOccupant)
        {
            Vector3Int gridPosition = _grid.ToCellPosition(worldPosition);

            if (IsValidCell(gridPosition, out BaseCell cell) == false)
                return;

            cell.Occupy(cellOccupant);
            CellOccupied?.Invoke();
        }

        private void RegisterOccupiedCell(BaseCell cell)
        {
            cell.Occupied -= RegisterOccupiedCell;
            _occupiedCells.Add(cell);
        }

        private bool IsValidCell(Vector3Int gridPosition, out BaseCell cell) =>
            _map.TryGetValue(gridPosition, out cell) == true && cell.IsOccupied == false;
    }
}
