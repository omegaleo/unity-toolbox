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
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                var obj = parent.transform.GetChild(i);
                if (Application.isPlaying)
                {
                    Object.Destroy(obj.gameObject);
                }
                else
                {
                    Object.DestroyImmediate(obj.gameObject);
                }
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

        public static bool TryGetComponent<T>(this GameObject gameObject, out T component) where T : Component
        {
            component = gameObject.GetComponent<T>();
            
            if (component != null)
            {
                return true;
            }

            return false;
        }
    }
}