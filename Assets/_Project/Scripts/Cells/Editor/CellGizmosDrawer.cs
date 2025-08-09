using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public class CellGizmosDrawer
    {
        private const float Height = 0.05f;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(BaseCell))]
        private static void DrawGizmos(BaseCell cell, GizmoType gizmoType)
        {
            Gizmos.color = cell.IsOccupied == true ? Color.red : Color.white;

            Vector3 center = cell.transform.position.Add(y: Height * 0.5f);
            Vector3 size = cell.transform.localScale.With(y: Height);

            Gizmos.DrawWireCube(center, size);
        }
    }
}
