using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class TraceFactory : IInitializable
    {
        private const string ParentName = "Traces";

        private readonly Dictionary<DirectionInfo, TraceSelectionRule> _cachedRules = new Dictionary<DirectionInfo, TraceSelectionRule>();
        private readonly MaterialPropertyBlock _materialPropertyBlock = new MaterialPropertyBlock();

        private readonly IAssetLoader _assetLoader;
        private readonly ILogger _logger;

        private TraceConfig _config;
        private Transform _parent;

        public TraceFactory(IAssetLoader assetLoader, ILogger logger)
        {
            _assetLoader = assetLoader;
            _logger = logger;
        }

        public void Initialize()
        {
            _config = _assetLoader.Load<TraceConfig>(AssetPaths.TraceConfigPath);

            foreach (TraceSelectionRule rule in _config.SelectionRules)
                _cachedRules.TryAdd(rule.DirectionInfo, rule);
        }

        public Trace CreateTrace(DirectionInfo directionInfo)
        {
            if (_parent == null)
                _parent = new GameObject(ParentName).transform;

            Quaternion rotation = GetTraceRotation(directionInfo);
            Trace trace = Object.Instantiate(_config.Prefab, Vector3.down, rotation, _parent);
            trace.Initialize(_materialPropertyBlock);

            return trace;
        }

        private Quaternion GetTraceRotation(DirectionInfo directionInfo)
        {
            if (_cachedRules.TryGetValue(directionInfo, out TraceSelectionRule selectionRule) == false)
            {
                _logger.Log($"Can't find the rule! From <{directionInfo.Previous}> to <{directionInfo.Current}> direction");
                return Quaternion.identity;
            }

            return selectionRule.Rotation;
        }
    }
}
