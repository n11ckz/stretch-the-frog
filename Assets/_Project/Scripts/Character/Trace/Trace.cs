using UnityEngine;

namespace Project
{
    [SelectionBase, RequireComponent(typeof(BoxCollider), typeof(GPUInstancingEnabler))]
    public class Trace : MonoBehaviour 
    {
        private GPUInstancingEnabler _instancingEnabler;

        public Vector3 PositionOffset => transform.position.With(y: transform.localScale.y / 2);

        public void Initialize(MaterialPropertyBlock materialPropertyBlock)
        {
            _instancingEnabler = GetComponent<GPUInstancingEnabler>();
            _instancingEnabler.Enable(materialPropertyBlock);
        }
    }
}
