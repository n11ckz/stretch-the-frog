using UnityEngine;

namespace Project
{
    public class ScrollableTab : MonoBehaviour
    {
        [field: SerializeField] public ScrollableTabType Type { get; private set; }

        public int Index { get; set; }
        public RectTransform RectTransform => transform as RectTransform;
        public bool IsDisabled => gameObject.activeSelf == false;
    }
}
