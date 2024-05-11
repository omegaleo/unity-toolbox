using System;
using System.Collections.Generic;
using System.Linq;
using UnityFlow.DocumentationHelper.Library.Documentation;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    [Documentation("", "Set of extension methods to help with Arrays.")]
    public static class ArrayExtensions
    {
        // Idea obtained from TaroDev's video on things to do in Unity - https://youtu.be/Ic5ux-tpkCE?t=302
        [Documentation("Next", @"Method to obtain the next object in the array by passing the current index.
Note: Idea obtained from TaroDev's video on things to do in Unity - https://youtu.be/Ic5ux-tpkCE?t=302", new []
        {
            "CurrentIndex - The index we're currently on inside the array passed as reference so we can automatically assign to it the next index"
        })]
        public static T Next<T>(this T[] array, ref int currentIndex)
        {
            return array[currentIndex++ % array.Length];
        }
        
        /// <summary>
        /// Method to obtain a random element from inside a list
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Random<T>(this T[] array)
        {
            if (array.Length == 0) return default;

            // Updated based on Robin King's tip about random items https://twitter.com/quoxel/status/1729137730607841755/photo/1
            int seed = (int)DateTime.Now.Ticks;
            
            var r = UnityEngine.Random.Range(0, array.Length);

            var returnValue = array[r];

            return returnValue ?? array.FirstOrDefault();

        }
    
        /// <summary>
        /// Method to obtain <paramref name="count"/> elements from inside a list
        /// </summary>
        /// <param name="array"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T[] Random<T>(this T[] array, int count = 1)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
            
            List<T> values = new List<T>();

            for (int i = 0; i < count; i++)
            {
                values.Add(array.Where(x => !values.Contains(x)).ToList().Random());
            }
        
            return values.ToArray();
        }
    }
}