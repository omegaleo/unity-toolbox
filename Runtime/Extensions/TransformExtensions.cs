using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroy all children of a Transform
        /// </summary>
        /// <param name="parent"></param>
        public static void DestroyChildren(this Transform parent)
        {
            parent.gameObject.DestroyChildren();
        }
    }
}