using UnityEditor;
using UnityEngine;

namespace Project
{
    public class CellGizmosDrawer
    {
        private const float Height = 0.05f;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(BaseCell))]
        private static void DrawGizmos(BaseCell cell, GizmoType gizmoType)
        {
            Gizmos.color = cell.IsOccupied ? Color.red : Color.white;

            Vector3 position = cell.transform.position.Add(y: Height * 0.5f);
            Vector3 size = cell.transform.localScale.With(y: Height);

            Gizmos.DrawWireCube(position, size);
        }
    }
}
