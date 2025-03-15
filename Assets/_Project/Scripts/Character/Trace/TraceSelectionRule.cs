using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Configs/Trace/" + nameof(TraceSelectionRule), fileName = nameof(TraceSelectionRule))]
    public class TraceSelectionRule : ScriptableObject
    {
        [field: SerializeField] public DirectionInfo DirectionInfo { get; private set; }

        [SerializeField] private Vector3 _eulerRotation;

        public Quaternion Rotation => Quaternion.Euler(_eulerRotation);
    }
}
