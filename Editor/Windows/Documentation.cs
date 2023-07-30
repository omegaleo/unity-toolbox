using System;
using System.Collections.Generic;
using System.Linq;
using Omega_Leo_Toolbox.Editor.Models;
using OmegaLeo.Toolbox.Editor.Helpers;
using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEditor;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Helpers;
using UnityFlow.DocumentationHelper.Library.Models;
using File = System.IO.File;

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
        private static List<Docs> _documentation = new List<Docs>();
        private static string _docsFilePath = "Assets/Omega Leo Toolbox/docs/docs.md";
        private static string _docsTxtFilePath = "Assets/Omega Leo Toolbox/docs/docs.txt";
        private static string _docsSidebarTxtFilePath = "Assets/Omega Leo Toolbox/docs/sidebar.txt";

        private static void LoadDocumentation()
        {
            _docs = DocumentationHelperTool.GenerateDocumentation(false);
            
            _documentation = new List<Docs>();
            
            foreach (var doc in _docs)
            {
                var addAssemblyDoc = false;
                
                var assemblyDoc = _documentation.FirstOrDefault(x => x.AssemblyName == doc.AssemblyName);

                if (assemblyDoc == null)
                {
                    assemblyDoc = new Docs(doc.AssemblyName);
                    addAssemblyDoc = true;
                }

                var addNamespaceDoc = false;
                
                var namespaceDoc = assemblyDoc.Namespaces.FirstOrDefault(x => x.Name == doc.Namespace);

                if (namespaceDoc == null)
                {
                    namespaceDoc = new DocNamespace(doc.Namespace);
                    addNamespaceDoc = true;
                }
                
                var addClassDoc = false;
                
                var classDoc = namespaceDoc.Classes.FirstOrDefault(x => x.Name == doc.ClassName);

                if (classDoc == null)
                {
                    classDoc = new DocClass(doc.ClassName);
                    addClassDoc = true;
                }

                foreach (var desc in doc.Descriptions)
                {
                    var args = Array.Empty<DocArgs>();
                    
                    if (desc.Args != null)
                    {
                        args = desc.Args.Select(x =>
                        {
                            var array = x.Split('-');

                            if (array.Length > 1)
                            {
                                return new DocArgs(array[0], array[1]);
                            }
                            else
                            {
                                return new DocArgs("", array[0]);
                            }
                        }).ToArray();
                    }

                    var addContent = false;
                    var content = classDoc.Contents.FirstOrDefault(x => x.Name == desc.Title);

                    if (content == null)
                    {
                        content = new DocContent(desc.Title, desc.Description, args);
                        addContent = true;
                    }
                    content.Description = desc.Description;

                    if (desc.Args != null)
                    {
                        content.Args = args;
                    }

                    if (addContent)
                    {
                        classDoc.Contents.Add(content);
                    }
                }

                if (addClassDoc)
                {
                    namespaceDoc.Classes.Add(classDoc);
                }

                if (addNamespaceDoc)
                {
                    assemblyDoc.Namespaces.Add(namespaceDoc);
                }

                if (addAssemblyDoc)
                {
                    _documentation.Add(assemblyDoc);
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
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reload docs"))
            {
                LoadDocumentation();
            }
            if (GUILayout.Button("Save to docs.md"))
            {
                File.WriteAllText(_docsFilePath, string.Join(Environment.NewLine, _documentation.Select(x => x.GenerateMarkdown())));
            }
            if (GUILayout.Button("Save to docs.txt"))
            {
                File.WriteAllText(_docsTxtFilePath, string.Join(Environment.NewLine, _documentation.Select(x => x.GenerateHtml())));
                File.WriteAllText(_docsSidebarTxtFilePath, string.Join(Environment.NewLine, _documentation.Select(x => x.GenerateSidebarHtml())));
            }
            EditorGUILayout.EndHorizontal();
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(Screen.height - 50));
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextArea(string.Join(Environment.NewLine, _documentation.Select(x => x.GenerateMarkdown())), GUILayout.ExpandHeight(true));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndScrollView();
        }
    }
}