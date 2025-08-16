using System.Linq;
using UnityEngine;

namespace Project
{
    public class LevelBorders : MonoBehaviour
    {
        [SerializeField] private BoxCollider[] _colliders;

        private void Reset() =>
            _colliders = GetComponentsInChildren<BoxCollider>(false);

        private void OnDrawGizmos()
        {
            Bounds bounds = CalculateBounds();
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        public Bounds CalculateBounds()
        {
            Vector3 startPoint = _colliders.First().transform.position;
            Bounds bounds = new Bounds(startPoint, Vector3.zero);

            for (int i = 0; i < _colliders.Length; i++)
                bounds.Encapsulate(_colliders[i].bounds);

            return bounds;
        }
    }
}
