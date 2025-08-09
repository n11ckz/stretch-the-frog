using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class TraceFactory : IInitializable
    {
        private readonly TraceConfig _traceConfig;
        
        private IReadOnlyDictionary<DirectionInfo, Quaternion> _rotationMap;
        private MaterialPropertyBlock _materialPropertyBlock;
        private Transform _traceContainer;

        public TraceFactory(TraceConfig traceConfig) =>
            _traceConfig = traceConfig;

        public void Initialize()
        {
            _rotationMap = _traceConfig.GetRotationMap();
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        public Trace CreateTrace(DirectionInfo info)
        {
            SetupContainer();

            if (_rotationMap.TryGetValue(info, out Quaternion rotation) == false)
                Logger.Log($"Can't find rule! From <{info.Previous}> to <{info.Current}>", LogMode.Warning);

            Trace trace = Object.Instantiate(_traceConfig.Prefab, Vector3.down, Quaternion.identity, _traceContainer);
            trace.Initialize(rotation, _materialPropertyBlock);

            return trace;
        }

        private void SetupContainer()
        {
#if UNITY_EDITOR
            if (_traceContainer != null)
                return;

            _traceContainer = new GameObject(_traceConfig.ContainerName).transform;
#endif
        }
    }
}
