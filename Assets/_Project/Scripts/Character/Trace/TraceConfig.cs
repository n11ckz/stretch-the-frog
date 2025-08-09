using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = nameof(TraceConfig), menuName = "Configs/Trace/" + nameof(TraceConfig))]
    public class TraceConfig : ScriptableObject
    {
        [field: SerializeField] public Trace Prefab { get; private set; }
        [field: SerializeField] public string ContainerName { get; private set; }

        [SerializeField] private List<TraceChoiceRule> _rules = new List<TraceChoiceRule>();

        public IReadOnlyDictionary<DirectionInfo, Quaternion> GetRotationMap()
        {
            Dictionary<DirectionInfo, Quaternion> rotationMap = new Dictionary<DirectionInfo, Quaternion>();

            foreach (TraceChoiceRule rule in _rules)
            {
                Quaternion rotation = Quaternion.Euler(rule.EulerRotation);
                rotationMap.TryAdd(rule.Info, rotation);
            }

            return rotationMap;
        }
    }
}
