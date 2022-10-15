using System.Collections.Generic;
using System.Linq;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// Calculate the average value based on a list of values
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int AverageWithNullValidation(this List<int> list)
        {
            return list.Count > 0 ? (int)list.Average() : 0;
        }

        /// <summary>
        /// Calculate the average value based on a list of values
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double AverageWithNullValidation(this List<double> list)
        {
            return list.Count > 0 ? list.Average() : 0.0;
        }

        /// <summary>
        /// Calculate the average value based on a list of values
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static float AverageWithNullValidation(this List<float> list)
        {
            return list.Count > 0 ? list.Average() : 0.0f;
        }
    }
}