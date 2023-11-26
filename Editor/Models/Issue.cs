using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OmegaLeo.Toolbox.Editor.Settings;
using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Models
{
    public class Issue
    {
        public string Title { get; set; }    
        public string Body { get; set; }
        public List<Label> Labels { get; set; }
        
        public string Html_Url { get; set; }
        public string Url { get; set; }
        public int Number { get; set; }

        public void GenerateIssueBox()
        {
            var headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = Color.white;
            
            var bodyStyle = new GUIStyle(GUI.skin.label);
            bodyStyle.normal.textColor = Color.white;
            bodyStyle.stretchHeight = true;
            bodyStyle.wordWrap = true;
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(Title, headerStyle);

            if (Body.IsNotNullOrEmpty())
            {
                EditorGUILayout.LabelField(Body.Replace("\n", Environment.NewLine), bodyStyle);
            }

            EditorGUILayout.BeginHorizontal();
            foreach (var label in Labels)
            {
                label.GenerateLabel();
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }

        public bool CanGoBack()
        {
            var order = GithubSettingsProvider.GithubTagOrder.Split(',').ToList();

            var value = false;

            foreach (var label in Labels)
            {
                if (order.Any(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    if (order.FindIndex(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase)) > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public void GoBack()
        {
            var order = GithubSettingsProvider.GithubTagOrder.Split(',').ToList();

            var nextValue = "";
            var labelToReplace= Labels.FirstOrDefault();
            
            foreach (var label in Labels)
            {
                if (order.Any(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    var currentOrder = order.FindIndex(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase));
                    var nextOrder = currentOrder - 1;
                    nextValue = order[nextOrder];
                    labelToReplace = label;
                }
            }
            
            Labels = Labels.Replace(labelToReplace, new Label() { Color = "", Name = nextValue }).ToList();
        }

        public void GoForward()
        {
            var order = GithubSettingsProvider.GithubTagOrder.Split(',').ToList();

            var nextValue = "";
            var labelToReplace= Labels.FirstOrDefault();
            
            foreach (var label in Labels)
            {
                if (order.Any(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    var currentOrder = order.FindIndex(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase));
                    var nextOrder = currentOrder + 1;
                    nextValue = order[nextOrder];
                    labelToReplace = label;
                }
            }
            
            Labels = Labels.Replace(labelToReplace, new Label() { Color = "", Name = nextValue }).ToList();
        }
        
        public bool CanGoForward()
        {
            var order = GithubSettingsProvider.GithubTagOrder.Split(',').ToList();

            var value = false;

            foreach (var label in Labels)
            {
                if (order.Any(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    if (order.FindIndex(x => x.Equals(label.Name, StringComparison.OrdinalIgnoreCase)) < order.Count)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class Label
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public void GenerateLabel()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            var color = $"#{Color}";
            style.normal.textColor = color.ColorFromHex();
            
            EditorGUILayout.LabelField(Name, style);
        }
    }
}