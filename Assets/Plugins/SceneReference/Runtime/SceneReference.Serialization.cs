#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace n11ckz.SceneReference
{
    public partial class SceneReference : ISerializationCallbackReceiver
    {
        [SerializeField] private SceneAsset _sceneAsset;

        [Obsolete("This method will not be included in build", true)]
        public void OnBeforeSerialize() =>
            SetValues();

        [Obsolete("This method will not be included in build", true)]
        public void OnAfterDeserialize() =>
            EditorApplication.update += SetValues;

        private void SetValues()
        {
            EditorApplication.update -= SetValues;

            if (_sceneAsset == null)
            {
                SetInvalidValues();
                return;
            }

            Name = _sceneAsset.name;
            BuildIndex = GetSceneBuildIndex();
        }

        private void SetInvalidValues()
        {
            Name = Constants.UndefinedSceneName;
            BuildIndex = Constants.InvalidSceneBuildIndex;
        }

        private int GetSceneBuildIndex()
        {
            string sceneAssetPath = AssetDatabase.GetAssetOrScenePath(_sceneAsset);
            return SceneUtility.GetBuildIndexByScenePath(sceneAssetPath);
        }
    }
}
#endif
