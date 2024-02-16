using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Documentation;

namespace OmegaLeo.Toolbox.Attributes
{
    [Documentation("ColoredHeaderAttribute", 
        "Attribute made specifically to implement fully custom colored headers in the inspector window.", 
        new []
        {
            "Title - Title to be displayed in the header",
            "Thickness - Thickness of the header in integer",
            "Padding - Inner padding of the header text",
            "TextColor - Color of the text",
            "BackgroundColor - Color for the background of the header",
            "IconPath - Path to the icon inside the project(can also be Unity Editor Built-in icon path, read here: <a href=\"https://github.com/halak/unity-editor-icons\">Unity Editor Icons</a>)"
        })]
    public class ColoredHeaderAttribute : PropertyAttribute
    {
        public int Thickness;
        public float Padding;
        public string Title;
        private readonly string _textColor;
        public UnityEngine.Color TextColor => _textColor.ColorFromHex();
        private readonly string _backgroundColor;
        public UnityEngine.Color BackgroundColor => _backgroundColor.ColorFromHex();
        public readonly string _iconPath = "";
        
        /// <summary>
        /// Attribute made specifically to implement fully custom colored headers in the inspector window.
        /// </summary>
        /// <param name="title">Title to be displayed in the header</param>
        /// <param name="thickness">Thickness of the header in integer</param>
        /// <param name="padding">Inner padding of the header text</param>
        /// <param name="textColor">Color of the text</param>
        /// <param name="backgroundColor"></param>
        /// <param name="iconPath">Path to the icon inside the project(can also be Unity Editor Built-in icon path, read here: https://github.com/halak/unity-editor-icons)</param>
        public ColoredHeaderAttribute(string title, int thickness = 1, float padding = 10f, string textColor = "#fcba03", string backgroundColor = "#000", string iconPath = "")
        {
            Title = title;
            Thickness = thickness;
            Padding = padding;
            _textColor = textColor;
            _backgroundColor = backgroundColor;
            _iconPath = iconPath;
        }
    
    }
}