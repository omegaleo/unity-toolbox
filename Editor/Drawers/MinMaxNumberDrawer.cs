using OmegaLeo.Toolbox.Runtime.Models;
using UnityEditor;
using UnityEngine;

namespace pt.omegaleo.utils.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(MinMaxNumber))]
    public class MinMaxNumberDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the Min and Max properties from the SerializedProperty
            SerializedProperty minProp = property.FindPropertyRelative("Min");
            SerializedProperty maxProp = property.FindPropertyRelative("Max");

            var minVal = minProp.floatValue;
            var maxVal = maxProp.floatValue;

            label.tooltip = $"Range from {minVal:F2} to {maxVal:F2}";
            
            // Draw the label on the first line
            Rect labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, label);

            // Move to the next line
            position.y += EditorGUIUtility.singleLineHeight;

            // Draw Min field
            Rect minRect = new Rect(position.x, position.y, position.width * 0.5f, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(minRect, minProp, new GUIContent("Min"));

            // Draw Max field
            Rect maxRect = new Rect(position.x + position.width * 0.5f, position.y, position.width * 0.5f,
                EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(maxRect, maxProp, new GUIContent("Max"));

            // Move to the next line
            position.y += EditorGUIUtility.singleLineHeight;

            // Draw MinMaxSlider
            Rect sliderRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.BeginChangeCheck();
            EditorGUI.MinMaxSlider(sliderRect, ref minVal, ref maxVal, minProp.floatValue - 10f,
                maxProp.floatValue + 10f);
            
            if (EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = minVal;
                maxProp.floatValue = maxVal;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3; // Adjust the height according to the number of lines
        }
    }
}