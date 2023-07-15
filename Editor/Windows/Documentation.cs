using System;
using System.Collections;
using System.Collections.Generic;
using OmegaLeo.Toolbox.Editor.Helpers;
using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEditor;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Helpers;
using UnityFlow.DocumentationHelper.Library.Models;

namespace OmegaLeo.Toolbox.Editor.Windows
{
    public class Documentation: EditorWindow
    {
        [MenuItem("Window/Documentation")]
        static void Init()
        {
            LoadDocumentation();
            // Get existing open window or if none, make a new one:
            Documentation window = (Documentation)EditorWindow.GetWindow(typeof(Documentation), false, "Documentation");
            window.name = "Build";
            window.titleContent = EditorGuiLayoutHelper.GetIconContent("Documentation", "CustomTool");
            window.Show();
        }

        private static IEnumerable<DocumentationStructure> _docs = new List<DocumentationStructure>();
        private static string _documentation = "";

        private static void LoadDocumentation()
        {
            _docs = DocumentationHelperTool.GenerateDocumentation(false);
            
            _documentation = "";

            foreach (var doc in _docs)
            {
                _documentation += $"# {doc.AssemblyName}{Environment.NewLine}  ";
                _documentation += $"## {doc.ClassName}{Environment.NewLine}  ";

                foreach (var desc in doc.Descriptions)
                {
                    if (desc.Title.IsNotNullOrEmpty())
                        _documentation += $"### {desc.Title}{Environment.NewLine}  ";
                    _documentation += $"{desc.Description}{Environment.NewLine}  ";
                }
            }
        }

        private Vector2 _scrollPosition = Vector2.zero;

        private void OnFocus()
        {
            LoadDocumentation();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Reload docs"))
            {
                LoadDocumentation();
            }
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(200));
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextArea(_documentation, GUILayout.ExpandHeight(true));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndScrollView();
        }
    }
}