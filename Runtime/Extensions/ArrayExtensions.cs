﻿using System;
using UnityFlow.DocumentationHelper.Library.Documentation;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    [Documentation("", "Set of extension methods to help with Arrays.", new []{nameof(ArrayExtensions)})]
    public static class ArrayExtensions
    {
        // Idea obtained from TaroDev's video on things to do in Unity - https://youtu.be/Ic5ux-tpkCE?t=302
        [Documentation("", @"Method to obtain the next object in the array by passing the current index.
Note: Idea obtained from TaroDev's video on things to do in Unity - https://youtu.be/Ic5ux-tpkCE?t=302", new []{nameof(ArrayExtensions)})]
        public static T Next<T>(this T[] array, ref int currentIndex)
        {
            return array[currentIndex++ % array.Length];
        }
    }
}