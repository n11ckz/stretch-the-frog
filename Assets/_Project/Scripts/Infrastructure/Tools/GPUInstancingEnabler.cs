using UnityEngine;

namespace Project
{
    public class GPUInstancingEnabler : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _meshRenderers;
        [SerializeField] private bool _isEnabledOnAwake;

        private void Awake()
        {
            if (_isEnabledOnAwake == false)
                return;

            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            Enable(materialPropertyBlock);
        }

        public void Enable(MaterialPropertyBlock materialPropertyBlock)
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
                meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
