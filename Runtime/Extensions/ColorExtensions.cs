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
        
        /// <summary>
        /// Creates a texture of width x height of a color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Texture2D MakeTex(this Color color, int width, int height)
        {
            Color[] pix = new Color[width*height];
 
            for(int i = 0; i < pix.Length; i++)
                pix[i] = color;
 
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
 
            return result;
        }
    }
}