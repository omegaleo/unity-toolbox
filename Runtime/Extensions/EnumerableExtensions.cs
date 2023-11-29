using System.Collections.Generic;
using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Copy<T>(this IEnumerable<T> array) where T : ScriptableObject
        {
            foreach (var obj in array)
            {
                yield return obj.Copy();
            }
        }
    }
}