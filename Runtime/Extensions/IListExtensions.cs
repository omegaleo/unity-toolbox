using System;
using System.Collections.Generic;
using System.Linq;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class IListExtensions
    {
        /// <summary>
        /// Swaps the position of 2 elements inside of a List
        /// </summary>
        /// <param name="list"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>List with swapped elements</returns>
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
            return list;
        }
        
        /// <summary>
        /// Swaps the position of 2 elements inside of a List
        /// </summary>
        /// <param name="list"></param>
        /// <param name="itemA"></param>
        /// <param name="itemB"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>List with swapped elements</returns>
        public static IList<T> Swap<T>(this IList<T> list, T itemA, T itemB)
        {
            return list.Swap(list.IndexOf(itemA), list.IndexOf(itemB));
        }
        
        /// <summary>
        /// Replace an element inside a list with a different element
        /// </summary>
        /// <param name="list"></param>
        /// <param name="originalValue"></param>
        /// <param name="valueToReplace"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Replace<T>(this IList<T> list, T originalValue, T valueToReplace)
        {
            var index = list.IndexOf(originalValue);

            if (index != -1)
            {
                list[index] = valueToReplace;
            }

            return list;
        }

        /// <summary>
        /// Method to obtain a random element from inside a list
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Random<T>(this IList<T> list)
        {
            if (list.Count == 0) return list[0];
            
            var r = new System.Random();

            var randomIndex = r.Next(list.Count);

            var returnValue = list[randomIndex];

            return returnValue ?? list[0];

        }
    
        /// <summary>
        /// Method to obtain <paramref name="count"/> elements from inside a list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static List<T> Random<T>(this IList<T> list, int count = 1)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
            
            List<T> values = new List<T>();

            for (int i = 0; i < count; i++)
            {
                values.Add(list.Where(x => !values.Contains(x)).ToList().Random());
            }
        
            return values;
        }
    }
}