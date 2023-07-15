using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Documentation;

namespace OmegaLeo.Toolbox.Runtime.Models
{
    /// <summary>
    /// Create an instanced MonoBehaviour of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Documentation("InstancedBehavior<T>", "Create an instanced MonoBehaviour of type T which can than be called by using <ClassName>.instance", new []{nameof(InstancedBehavior<T>)})]
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