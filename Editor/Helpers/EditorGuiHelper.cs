using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Helpers
{
    public static class EditorGuiHelper
    {
        private static GUIStyle _horizontalLineStyle = new GUIStyle()
        {
            normal = new GUIStyleState()
            {
                background = EditorGUIUtility.whiteTexture,
            },
            margin = new RectOffset(0, 0, 4, 4),
            fixedHeight = 1
        };

        public static void HorizontalLine(Color color)
        {
            var c = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, _horizontalLineStyle);
            GUI.color = c;
        }
    }
}