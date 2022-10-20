using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEngine;
namespace OmegaLeo.Toolbox.Editor.Attributes
{
    public class ColoredHeaderAttribute : PropertyAttribute
    {
        public int Thickness;
        public float Padding;
        public string Title;
        public UnityEngine.Color TextColor;
        public UnityEngine.Color BackgroundColor;
    
        public ColoredHeaderAttribute(string title, int thickness = 1, float padding = 10f, string textColor = "#fcba03", string backgroundColor = "#000")
        {
            Title = title;
            Thickness = thickness;
            Padding = padding;
            TextColor = textColor.ColorFromHex();
            BackgroundColor = backgroundColor.ColorFromHex();
        }
    
    }
}