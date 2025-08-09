using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace n11ckz.SceneReference.Editor
{
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferenceDrawer : PropertyDrawer
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            TryLoadVisualTreeAsset();

            VisualElement root = _visualTreeAsset.CloneTree();

            List<Foldout> foldouts = root.Query<Foldout>().ToList();
            RegisterFoldoutValueStates(foldouts, property);

            Foldout foldout = foldouts.First();
            ChangeRootFoldoutDisplayName(foldout, property);

            return root;
        }

        private void TryLoadVisualTreeAsset()
        {
            if (_visualTreeAsset != null)
                return;

            _visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Paths.Tree);
        }

        private void RegisterFoldoutValueStates(IEnumerable<Foldout> foldouts, SerializedProperty property)
        {
            int id = property.serializedObject.targetObject.GetInstanceID();

            foreach (Foldout foldout in foldouts)
            {
                string key = $"{id}-{property.propertyPath}-{foldout.name}-state";

                foldout.value = EditorPrefs.GetBool(key, true);
                foldout.RegisterValueChangedCallback((x) =>
                {
                    if (x.target != foldout)
                        return;

                    EditorPrefs.SetBool(key, x.newValue);
                });
            }
        }

        private void ChangeRootFoldoutDisplayName(Foldout foldout, SerializedProperty property)
        {
            foldout.TrackPropertyValue(property, (x) => foldout.text = x.displayName);
            foldout.text = property.displayName;
        }
    }
}
