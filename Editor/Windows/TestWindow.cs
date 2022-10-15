using System;
using OmegaLeo.Toolbox.Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Windows
{
    public class TestWindow : EditorWindow
    {
        [MenuItem("OmegaLeo/Test")]
        public static void Init()
        {
            // Get existing open window or if none, make a new one:
            TestWindow window = (TestWindow)EditorWindow.GetWindow(typeof(TestWindow), false, "Test");
            window.name = "TestWindow";
            //window.titleContent = CustomEditorUtility.GetIconContent("Build", "CustomTool");
            window.Show();
        }

        private void OnGUI()
        {
            EditorGuiHelper.HorizontalLine(Color.gray);
            EditorGUILayout.LabelField("Test");
            EditorGuiHelper.HorizontalLine(Color.gray);
        }
    }
}