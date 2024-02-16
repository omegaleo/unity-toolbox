using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class FloatExtensions
    {
        public static float Round(this float value)
        {
            return Mathf.Round(value);
        }

        public static float RoundToInt(this float value)
        {
            return Mathf.RoundToInt(value);
        }
    }
}