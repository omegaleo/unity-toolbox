
using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Events;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityFlow.DocumentationHelper.Library.Documentation;
using Object = UnityEngine.Object;

namespace OmegaLeo.Toolbox.Editor.Extensions
{
    [Documentation(nameof(ExtractComponentToChildTool), "Extension to the component menu that allows the extraction of the component to a child of the game object and associates all button onclick events that mentioned the old component to the new component that was extracted.<br>Based on a <a target='_blank' href='https://www.youtube.com/watch?v=qDoevls1wmI'>video</a> by <a target='_blank' href='https://www.youtube.com/@WarpedImagination'>Warped Imagination</a>")]
    public static class ExtractComponentToChildTool
    {
        [MenuItem("CONTEXT/Component/Extract", priority = 504)]
        public static void ExtractMenuOption(MenuCommand command)
        {
            var sourceComponent = command.context as Component;
            
            if (sourceComponent != null)
            {
                int undoGroupIndex = Undo.GetCurrentGroup();
                Undo.IncrementCurrentGroup();
                
                var child = new GameObject(sourceComponent.GetType().Name);
                child.transform.parent = sourceComponent.transform;
                child.transform.localScale = Vector3.zero;
                child.transform.localPosition = Vector3.zero;
                child.transform.localRotation = Quaternion.identity;

                Undo.RegisterCreatedObjectUndo(child, "Created child object");

                if (ComponentUtility.CopyComponent(sourceComponent) && ComponentUtility.PasteComponentAsNew(child))
                {
                    var newComponent = child.GetComponent(sourceComponent.GetType());
                
                    // Check for any association to the old source component and point them to new component
                    var buttons = Object.FindObjectsOfType<Button>();

                    foreach (var button in buttons)
                    {
                        var persistentEventCount = button.onClick.GetPersistentEventCount();

                        for (int i = 0; i < persistentEventCount; i++)
                        {
                            try
                            {
                                var persistentTarget = button.onClick.GetPersistentTarget(i);

                                if (persistentTarget == sourceComponent)
                                {
                                    var persistentMethodName = button.onClick.GetPersistentMethodName(i);
                                    var targetinfo = UnityEvent.GetValidMethodInfo(newComponent,
                                        persistentMethodName, new Type[]{});
                                    var methodDelegate = Delegate.CreateDelegate(typeof(UnityAction), newComponent, persistentMethodName) as UnityAction;
                                    UnityEventTools.RemovePersistentListener(button.onClick, i);
                                    UnityEventTools.AddPersistentListener(button.onClick, methodDelegate);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    }
                
                    // Destroy source component
                    Undo.DestroyObjectImmediate(sourceComponent);

                    // Ask the user if they want to add any already existing children to this new game object
                
                    // Set our undos for if the Undo shortcut is pressed
                    Undo.CollapseUndoOperations(undoGroupIndex);
                }
                else
                {
                    Debug.LogError("Failed to extract component", sourceComponent.gameObject);
                    Undo.CollapseUndoOperations(undoGroupIndex);
                    Undo.PerformUndo();
                }
            }
        }
    }
}