#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public partial class SceneReference : ISerializationCallbackReceiver
    {
        private const string UndefinedSceneName = "Undefined Scene";

        [SerializeField] private SceneAsset _sceneAsset;

        public void OnBeforeSerialize()
        {
            if (_sceneAsset == null)
            {
                SetInvalidValues();
                return;
            }

            Name = _sceneAsset.name;
            SetSceneBuildIndex();
        }

        public void OnAfterDeserialize() { }

        private void SetInvalidValues()
        {
            Name = UndefinedSceneName;
            BuildIndex = InvalidSceneBuildIndex;
        }

        private void SetSceneBuildIndex()
        {
            string scenePath = AssetDatabase.GetAssetOrScenePath(_sceneAsset);
            BuildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
        }
    }
}
#endif
