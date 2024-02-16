using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Helpers
{
    public static class EditorStylesHelper
    {
        public static GUIStyle HorizontalLineStyle => 
            new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    background = EditorGUIUtility.whiteTexture,
                },
                margin = new RectOffset(0, 0, 4, 4),
                fixedHeight = 1
            };

        public static GUIStyle TitleStyle =>
            new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.white
                },
                fontStyle = FontStyle.Bold,
                fontSize = 16,
                margin = new RectOffset(0, 0, 30, 30),
                padding = new RectOffset(0, 0, 30, 30),
                alignment = TextAnchor.MiddleCenter
            };

        public static GUIStyle SubTitleStyle =>
            new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.white
                },
                margin = new RectOffset(0, 0, 30, 30),
                richText = true
            };

        public static GUIStyle ButtonStyle =>
            new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.white
                },
                fontStyle = FontStyle.Bold,
                fontSize = 16,
                margin = new RectOffset(0, 0, 30, 30),
            };

        public static GUIStyle TabbedButtonStyle =>
            new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.white,
                    background = Texture2D.grayTexture
                },
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                margin = new RectOffset(0, 0, 10, 10),
                padding = new RectOffset(10, 10, 10, 10),
                border = new RectOffset(10, 10, 10, 10)
            };

        public static GUIStyle ActiveTabbedButtonStyle =>
            new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.white,
                    background = "#3d3d3d".ColorFromHex().GenerateTexture2D()
                },
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                margin = new RectOffset(0, 0, 10, 10),
                padding = new RectOffset(10, 10, 10, 10),
                border = new RectOffset(10, 10, 10, 10)
            };

        public static GUIStyle HeaderStyle(Color textColor = default, Color backgroundColor = default) =>
            new GUIStyle()
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState()
                {
                    textColor = textColor,
                    background = backgroundColor.GenerateTexture2D()
                }
            };
    }
}