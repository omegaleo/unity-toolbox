using System;

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
            return !str.IsNullOrEmpty();
        }

        /// <summary>
        /// Check if the string matches any of the search parameters
        /// </summary>
        /// <param name="str"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static bool AnyMatch(this string str, params string[] search)
        {
            foreach (string s in search)
            {
                if (s.Equals(str, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Reverses a given string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(this string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}