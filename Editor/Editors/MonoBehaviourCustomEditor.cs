using System;
using System.Linq;
using System.Reflection;
using OmegaLeo.Toolbox.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace OmegaLeo.Toolbox.Editor.Omega_Leo_Toolbox.Editor.Editors
{
    [CustomEditor(typeof(MonoBehaviour), true), CanEditMultipleObjects] // Target all MonoBehaviours and descendants
public class MonoBehaviourCustomEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draw the normal inspector

        // Get the type descriptor for the MonoBehaviour we are drawing
        var type = target.GetType();

        HandleMethods(type);
    }

    private void HandleMethods(Type type)
    {
        // Iterate over each private or public instance method (no static methods atm)
        foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance |
                                               BindingFlags.FlattenHierarchy))
        {
            // make sure it is decorated by our custom attribute
            var attributes = method.GetCustomAttributes(typeof(MethodButtonAttribute), true);
            if (attributes.Length > 0)
            {
                var attribute = attributes.Select(x => x as MethodButtonAttribute)
                    .FirstOrDefault(x => x.methodName == method.Name);
                if (GUILayout.Button(attribute.buttonName))
                {
                    // If the user clicks the button, invoke the method immediately.
                    // There are many ways to do this but I chose to use Invoke which only works in Play Mode.
                    method.Invoke(target, null);
                }
            }
        }
    }
}
}