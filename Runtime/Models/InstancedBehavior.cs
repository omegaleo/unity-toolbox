using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Models
{
    /// <summary>
    /// Create an instanced MonoBehaviour of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InstancedBehavior<T> : MonoBehaviour where T: Component
    {
        public static T instance;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
        }
    }
}