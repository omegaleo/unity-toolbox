using System;
using OmegaLeo.Toolbox.Attributes;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
    public class ConditionalFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var condHAtt = (ConditionalFieldAttribute)attribute;
            bool enabled = GetConditionalFieldAttributeResult(condHAtt, property);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (enabled)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            GUI.enabled = wasEnabled;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var condHAtt = (ConditionalFieldAttribute)attribute;
            bool enabled = GetConditionalFieldAttributeResult(condHAtt, property);

            if (enabled)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }

            // We return a height of 0 when the property is hidden
            return 0f;
        }

        private bool GetConditionalFieldAttributeResult(ConditionalFieldAttribute condField, SerializedProperty property)
        {
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condField.ConditionalSourceField);

            if (sourcePropertyValue != null)
            {
                (var sourceType, var sourceValue) = GetPropertyValue(sourcePropertyValue, condField.ConditionalSourceField);

                return AreValuesEqual(sourceValue, sourceType, condField.Value, condField.ValueType);
            }
            
            return true;
        }

        private bool AreValuesEqual(object sourceValue, Type sourceType, object condFieldValue, Type condFieldValueType)
        {
            if (sourceType == condFieldValueType || sourceType == typeof(Enum))
            {
                return sourceValue.ToString().Equals(condFieldValue.ToString(), StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private (Type, object) GetPropertyValue(SerializedProperty property, string propertyName)
        {
            SerializedProperty prop = property.serializedObject.FindProperty(propertyName);
            if (prop != null)
            {
                switch (prop.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        return (typeof(bool), prop.boolValue);
                    case SerializedPropertyType.Integer:
                        return (typeof(int), prop.intValue);
                    case SerializedPropertyType.Float:
                        return (typeof(float), prop.floatValue);
                    case SerializedPropertyType.String:
                        return (typeof(string), prop.stringValue);
                    case SerializedPropertyType.Enum:
                        return (typeof(Enum), prop.enumNames[prop.enumValueIndex]);
                    default:
                        Debug.LogWarning("ConditionalFieldAttribute: Property type not supported.");
                        break;
                }
            }
            else
            {
                Debug.LogWarning("ConditionalFieldAttribute: Property not found.");
            }
            
            return (null, null);
        }
    }
}