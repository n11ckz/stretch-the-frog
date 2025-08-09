using UnityEngine;

namespace Project
{
    public interface IGrid
    {
        public Vector3Int ToCellPosition(Vector3 worldPosition);
    }
}
