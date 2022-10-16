using System;
using System.Collections.Generic;
using System.Linq;
using Omega_Leo_Toolbox.Editor.Models;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Helpers
{
    public static class EditorGuiLayoutHelper
    {
        public static void HorizontalLine(Color color)
        {
            // Cache the original color
            var c = GUI.color;
            
            // Swap to the color we want
            GUI.color = color;
            
            // Draw the line with the style declared in EditorStylesHelper
            GUILayout.Box(GUIContent.none, EditorStylesHelper.HorizontalLineStyle);
            
            // Swap back the GUI Color
            GUI.color = c;
        }

        public static void ColoredHeader(string text,Color textColor, Color backgroundColor)
        {
            EditorGuiLayoutHelper.HorizontalLine(Color.gray);
            
            GUILayout.Box(text, EditorStylesHelper.HeaderStyle(textColor, backgroundColor));

            EditorGuiLayoutHelper.HorizontalLine(Color.gray);
        }
        
        public static GUIContent GetIconContent(string text, string iconName) =>
            new GUIContent(text, EditorGUIUtility.IconContent(iconName).image);
        
        public static GUIContent GetIconContent(string text, Texture2D icon) =>
            new GUIContent(text, icon);

        public static Texture2D GetIconTexture(string textureName) =>
            (Texture2D) AssetDatabase.LoadAssetAtPath($"Packages/pt.omegaleo.toolbox/Editor/Resources/Icons/{textureName}.png",
                typeof(Texture2D));

        public static int TabbedGroup(int currentTab, IEnumerable<Tab> tabs)
        {
            var enumerable = tabs.ToList();
            
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < enumerable.Count(); i++)
            {
                var tab = enumerable[i];
                
                if (GUILayout.Button(GetIconContent(tab.ButtonName, tab.Icon), 
                        (i == currentTab) ? EditorStylesHelper.ActiveTabbedButtonStyle : EditorStylesHelper.TabbedButtonStyle, 
                        GUILayout.Width(EditorGUIUtility.currentViewWidth / enumerable.Count), 
                        GUILayout.Height(48)))
                {
                    currentTab = i;
                }
            }
            EditorGUILayout.EndHorizontal();
            
            try
            {
                enumerable[currentTab].Action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
            return currentTab;
        }
    }
}