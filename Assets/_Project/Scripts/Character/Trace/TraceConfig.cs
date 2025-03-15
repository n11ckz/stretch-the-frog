using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Configs/Trace/" + nameof(TraceConfig), fileName = nameof(TraceConfig))]
    public class TraceConfig : ScriptableObject
    {
        [field: SerializeField] public Trace Prefab { get; private set; }

        [SerializeField] private List<TraceSelectionRule> _selectionRules = new List<TraceSelectionRule>();

        public IEnumerable<TraceSelectionRule> SelectionRules => _selectionRules;
    }
}
