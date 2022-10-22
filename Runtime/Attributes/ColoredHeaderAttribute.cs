using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEngine;
namespace OmegaLeo.Toolbox.Attributes
{
    public class ColoredHeaderAttribute : PropertyAttribute
    {
        public int Thickness;
        public float Padding;
        public string Title;
        private readonly string _textColor;
        public UnityEngine.Color TextColor => _textColor.ColorFromHex();
        private readonly string _backgroundColor;
        public UnityEngine.Color BackgroundColor => _backgroundColor.ColorFromHex();
        
        //TODO: Add attribute for icon to be shown in the header as well
        public ColoredHeaderAttribute(string title, int thickness = 1, float padding = 10f, string textColor = "#fcba03", string backgroundColor = "#000")
        {
            Title = title;
            Thickness = thickness;
            Padding = padding;
            _textColor = textColor;
            _backgroundColor = backgroundColor;
        }
    
    }
}