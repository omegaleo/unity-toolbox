﻿using System;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Cut off a string once it reaches a <paramref name="maxChars"/> amount of characters and add '...' to the end of the string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxChars"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
        
        /// <summary>
        /// Identify if a string is null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }
        
        /// <summary>
        /// Identify if a string is <b>not</b> null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !String.IsNullOrEmpty(str);
        }
    }
}