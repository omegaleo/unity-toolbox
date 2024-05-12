using OmegaLeo.Toolbox.Runtime.Models;
using UnityEditor;
using UnityEngine;

namespace pt.omegaleo.utils.Editor.Editors
{
    [CustomEditor(typeof(MinMaxNumber))]
    public class MinMaxNumberEditor : UnityEditor.Editor
    {
        private SerializedProperty minProp;
        private SerializedProperty maxProp;

        private void OnEnable()
        {
            // Initialize serialized properties
            minProp = serializedObject.FindProperty("Min");
            maxProp = serializedObject.FindProperty("Max");
        }

        public override void OnInspectorGUI()
        {
            // Update serialized object's representation
            serializedObject.Update();

            var minVal = minProp.floatValue;
            var maxVal = maxProp.floatValue;
            
            var label = new GUIContent(target.name);
            
            label.tooltip = minVal.ToString("F2") + " to " + maxVal.ToString("F2");

            //PrefixLabel returns the rect of the right part of the control. It leaves out the label section. We don't have to worry about it. Nice!
            EditorGUILayout.PrefixLabel(label);
            
            
            EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, minProp.floatValue, maxProp.floatValue);
            
            if (minVal < minProp.floatValue)
            {
                maxVal = minProp.floatValue;
            }

            if (minVal > maxProp.floatValue)
            {
                maxVal = maxProp.floatValue;
            }

            minProp.floatValue = minVal;
            maxProp.floatValue = maxVal;

            // Apply any changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}