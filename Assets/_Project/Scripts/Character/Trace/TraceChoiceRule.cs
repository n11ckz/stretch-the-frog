using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(TraceChoiceRule), menuName = "Configs/Trace/" + nameof(TraceChoiceRule))]
    public class TraceChoiceRule : ScriptableObject
    {
        [field: SerializeField] public DirectionInfo Info { get; private set; }
        [field: SerializeField] public Vector3 EulerRotation { get; private set; }
    } 
}
