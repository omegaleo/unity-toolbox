using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts a color to it's hexadecimal equivalent
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHex(this Color color)
        {
            return "#" + ColorUtility.ToHtmlStringRGBA(color);
        }
        
        /// <summary>
        /// Converts a hexadecimal color into a Color, if it can't parse correctly it'll return Black
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color ColorFromHex(this string value)
        {
            if (ColorUtility.TryParseHtmlString(value, out var newCol))
            {
                return newCol;
            }

            return Color.black;
        }
    }
}