/*
    Disclaimer: I do not own this code, only did some changes here and there, original code comes from 
    Warped Imagination's video on Scene Selection Overlay - https://www.youtube.com/watch?v=yqneLnM8syk
    and also Organize Generic Unity Menus with Submenus - https://www.youtube.com/watch?v=zeF3YtHtvf4
    Support them by going to their channel and giving them some love for their awesome tutorials.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OmegaLeo.Toolbox.Editor.Helpers;
using OmegaLeo.Toolbox.Runtime.Extensions;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OmegaLeo.Toolbox.Editor.Overlays
{
    [Overlay(typeof(SceneView), "Scenes")]
    [Icon(ICON)]
    public class SceneSelectionOverlay : ToolbarOverlay
    {
        public const string ICON = "Packages/pt.omegaleo.toolbox/Editor/Resources/Icons/Scenes.png";
        
        SceneSelectionOverlay() : base(SceneDropdownToggle.ID) {}

        [EditorToolbarElement(ID, typeof(SceneView))]
        class SceneDropdownToggle : EditorToolbarDropdownToggle, IAccessContainerWindow
        {
            public const string ID = "SceneSelectionOverlay/SceneDropdownToggle";
            
            public EditorWindow containerWindow { get; set; }

            SceneDropdownToggle()
            {
                text = "Scenes";
                tooltip = "Select a scene to load";

                if (SceneSelectionOverlay.ICON.IsNotNullOrEmpty())
                {
                    icon = EditorGuiLayoutHelper.GetIconTexture("Scenes");
                }

                dropdownClicked += ShowSceneMenu;
            }

            private void ShowSceneMenu()
            {
                var menu = new GenericMenu();

                var currentScene = EditorSceneManager.GetActiveScene();
                
                var sceneGuids = AssetDatabase.FindAssets("t:scene");

                foreach (var sceneGuid in sceneGuids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(sceneGuid);

                    var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);

                    if (String.CompareOrdinal(currentScene.name, scene.name) == 0)
                    {
                        menu.AddDisabledItem(new GUIContent(scene.name));
                    }
                    else
                    {
                        menu.AddItem(new GUIContent(scene.name + "/Single"), false, () => OpenScene(currentScene, path, OpenSceneMode.Single));
                        menu.AddItem(new GUIContent(scene.name + "/Additive"), false, () => OpenScene(currentScene, path, OpenSceneMode.Additive));
                    }
                }
                
                menu.ShowAsContext();
            }

            private void OpenScene(Scene currentScene, string path, OpenSceneMode mode)
            {
                if (currentScene.isDirty)
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(path, mode);
                    }
                }
                else
                {
                    EditorSceneManager.OpenScene(path, mode);
                }
            }
        }
    }
}
