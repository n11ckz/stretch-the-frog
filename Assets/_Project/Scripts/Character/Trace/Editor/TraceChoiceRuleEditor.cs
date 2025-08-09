using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Editor
{
    [CustomEditor(typeof(TraceChoiceRule))]
    public class TraceChoiceRuleEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = _visualTreeAsset.CloneTree();

            string infoPropertyName = nameof(TraceChoiceRule.Info).ToGeneratedFieldName();
            SerializedProperty infoProperty = serializedObject.FindProperty(infoPropertyName);
            BindInspectorFields(root, infoProperty);

            return root;
        }

        private void BindInspectorFields(VisualElement root, SerializedProperty property)
        {
            string fromPropertyName = nameof(DirectionInfo.Previous).ToGeneratedFieldName();
            SerializedProperty fromProperty = property.FindPropertyRelative(fromPropertyName);
            EnumField fromField = root.Q<EnumField>("from-field");
            fromField.BindProperty(fromProperty);

            string toPropertyName = nameof(DirectionInfo.Current).ToGeneratedFieldName();
            SerializedProperty toProperty = property.FindPropertyRelative(toPropertyName);
            EnumField toField = root.Q<EnumField>("to-field");
            toField.BindProperty(toProperty);
        }
    }
}
