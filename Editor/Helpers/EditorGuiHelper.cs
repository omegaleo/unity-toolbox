using UnityEditor;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Documentation;

namespace OmegaLeo.Toolbox.Editor.Helpers
{
    public static class EditorGuiHelper
    {
        // Code for GUI color obtained from Warped Imagination's video https://youtu.be/EFh7tniBqkk?feature=shared&t=514
        private static readonly Color _defaultColor = new Color(0.7843f, 0.7843f, 0.7843f);
        private static readonly Color _defaultProColor = new Color(0.2196f, 0.2196f, 0.2196f);
        
        private static readonly Color _selectedColor = new Color(0.22745f, 0.22745f, 0.22745f);
        private static readonly Color _selectedProColor = new Color(0.1725f, 0.1725f, 0.1725f);
        
        private static readonly Color _selectedUnfocusedColor = new Color(0.68f, 0.68f, 0.68f);
        private static readonly Color _selectedUnfocusedProColor = new Color(0.3f, 0.3f, 0.3f);
        
        private static readonly Color _hoveredColor = new Color(0.698f, 0.698f, 0.698f);
        private static readonly Color _hoveredProColor = new Color(0.2706f, 0.2706f, 0.2706f);

        private static Color GetGUIColor(Color regularColor, Color proColor) =>
            (EditorGUIUtility.isProSkin) ? proColor : regularColor;
        
        [Documentation(nameof(GuiColor), 
            "Based on Warped Imagination's code for GUI Color featured in this video https://youtu.be/EFh7tniBqkk?feature=shared&t=514")]
        public static Color GuiColor(bool isSelected, bool isHovered, bool isWindowFocused)
        {
            if (isSelected)
            {
                if (isWindowFocused)
                {
                    return GetGUIColor(_selectedColor, _selectedProColor);
                }
                else
                {
                    return GetGUIColor(_selectedUnfocusedColor, _selectedUnfocusedProColor);
                }
            }
            else if (isHovered)
            {
                return GetGUIColor(_hoveredColor, _hoveredProColor);
            }

            return GetGUIColor(_defaultColor, _defaultProColor);
        }
    }
}