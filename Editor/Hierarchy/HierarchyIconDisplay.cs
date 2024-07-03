using System.Linq;
using OmegaLeo.Toolbox.Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Hierarchy
{
    // Based on Warped Imagination's video https://www.youtube.com/watch?v=EFh7tniBqkk
    [InitializeOnLoad]
    public static class HierarchyIconDisplay
    {
        private static bool _hierarchyHasFocus = false;

        private static EditorWindow _hierarchyEditorWindow;
        
        static HierarchyIconDisplay()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if (_hierarchyEditorWindow == null)
            {
                _hierarchyEditorWindow =
                    EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));
            }

            _hierarchyHasFocus = EditorWindow.focusedWindow != null &&
                                 EditorWindow.focusedWindow == _hierarchyEditorWindow;
        }

        private static void OnHierarchyWindowItemGUI(int instanceId, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

            if (obj == null) return;

            var components = obj.GetComponents<Component>();

            if (components == null || !components.Any()) return;

            var component = components.Length > 1 ? components[1] : components[0];

            if (component == null) return;
            
            var type = component.GetType();

            var content = EditorGUIUtility.ObjectContent(component, type);
            content.text = null;
            content.tooltip = type.Name;

            if (content.image == null) return;

            var isSelected = Selection.instanceIDs.Contains(instanceId);
            var isHovering = selectionRect.Contains(Event.current.mousePosition);
            
            var color = EditorGuiHelper.GuiColor(isSelected, isHovering, _hierarchyHasFocus);
            var backgroundRect = selectionRect;
            backgroundRect.width = 18.5f;
            
            EditorGUI.DrawRect(backgroundRect, color);
            
            EditorGUI.LabelField(selectionRect, content);
        }
    }
}