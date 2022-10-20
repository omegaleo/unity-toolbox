using OmegaLeo.Toolbox.Attributes;
using OmegaLeo.Toolbox.Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace Omega_Leo_Toolbox.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ColoredHeaderAttribute))]
    public class ColoredHeaderDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            var attr = attribute as ColoredHeaderAttribute;
            return EditorStylesHelper.HeaderStyle().CalcHeight(new GUIContent(attr.Title), Screen.width) + Mathf.Max(attr.Padding, attr.Thickness);
        }

        public override void OnGUI(Rect position)
        {
            var attr = attribute as ColoredHeaderAttribute;

            position.y += attr.Padding * .5f;
            position.height = EditorStylesHelper.HeaderStyle().CalcHeight(new GUIContent(attr.Title), Screen.width);
            position.height += attr.Thickness;
            position.y += attr.Padding * .5f;

            EditorGUI.DrawRect(position, attr.BackgroundColor);
            EditorGUI.LabelField(position, attr.Title, EditorStylesHelper.HeaderStyle(attr.TextColor));
        }
    }
}