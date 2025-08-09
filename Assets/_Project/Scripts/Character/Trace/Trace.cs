using UnityEngine;

namespace Project
{
    [SelectionBase]
    public class Trace : MonoBehaviour
    {
        [SerializeField] private Transform _model;

        public void Initialize(Quaternion rotation, MaterialPropertyBlock materialPropertyBlock)
        {
            _model.rotation = rotation;
        }
    }
}
