using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Checks whether a game object has a component of type T attached.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }
        
        /// <summary>
        /// Destroy all children of a GameObject
        /// </summary>
        /// <param name="parent"></param>
        public static void DestroyChildren(this GameObject parent)
        {
            Transform[] children = new Transform[parent.transform.childCount];
            
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                children[i] = parent.transform.GetChild(i); 
            }

            for (int i = 0; i < children.Length; i++)
            {
                Object.Destroy(children[i].gameObject);
            }
        }
        
        /// <summary>
        /// Toggle the defined game object on and off
        /// </summary>
        /// <param name="gameObject"></param>
        public static void ToggleGameObject(this GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        
        
    }
}