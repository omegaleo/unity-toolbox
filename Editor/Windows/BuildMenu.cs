using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Omega_Leo_Toolbox.Editor.Models;
using OmegaLeo.Toolbox.Editor.Helpers;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Windows
{
    public class BuildMenu : EditorWindow
    {
        private static BuildSettings _settings;

        private int _selectedSettingsTab = 0;
        
        [MenuItem("Window/Omega Leo's Toolbox/Build")]
        static void Init()
        {
            LoadSettings();
            
            // Get existing open window or if none, make a new one:
            BuildMenu window = (BuildMenu)EditorWindow.GetWindow(typeof(BuildMenu), false, "Omega Leo Build Menu");
            window.name = "Build";
            window.titleContent = EditorGuiLayoutHelper.GetIconContent("Build", "CustomTool");
            window.Show();
        }

        private static void LoadSettings()
        {
            _settings = BuildSettingsHelper.GetOrCreateBuildSettings();
        }

        private void Awake()
        {
            LoadSettings();
        }
        
        void UpdateBuildNumber()
        {
            SaveProjectSettings();
        }

        void OnGUI()
        {
            if (_settings == null)
            {
                LoadSettings();
            }

            
            _selectedSettingsTab = EditorGuiLayoutHelper.TabbedGroup(_selectedSettingsTab, new[]
            {
                new Tab("General Build Settings", EditorGuiLayoutHelper.GetIconTexture("build"), () =>
                {
                    EditorGUILayout.LabelField("General Build Settings", EditorStylesHelper.TitleStyle);

                    EditorGUILayout.LabelField("Project Name:");
                    _settings.ProjectName = EditorGUILayout.TextField(_settings.ProjectName);

                    EditorGUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Company Name:");
                    _settings.CompanyName = EditorGUILayout.TextField(_settings.CompanyName);

                    EditorGUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Company Name to be used in UWP:");
                    _settings.CompanyNameForUWP = EditorGUILayout.TextField(_settings.CompanyNameForUWP);

                    EditorGUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Build Number:");
                    _settings.BaseBuildNumber = EditorGUILayout.TextField(_settings.BaseBuildNumber);
                    _settings.BuildNumber = EditorGUILayout.IntField(_settings.BuildNumber);
                    EditorGUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Bundle Version Code:");
                    _settings.BundleVersionCode = EditorGUILayout.IntField(_settings.BundleVersionCode);
                    EditorGUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Bundle Identifier:");
                    _settings.BundleIdentifier = EditorGUILayout.TextField(_settings.BundleIdentifier);
                    EditorGUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Key Store Password:");
                    _settings.KeyStorePassword = EditorGUILayout.PasswordField(_settings.KeyStorePassword);
                    EditorGUILayout.Space(2.0f);
                    EditorGUILayout.LabelField("Key Alias Password:");
                    _settings.KeyAliasPassword = EditorGUILayout.PasswordField(_settings.KeyAliasPassword);
                    EditorGUILayout.Space(2.0f);
                    if (GUILayout.Button("Save"))
                    {
                        SaveProjectSettings();
                    }
                }),
                new Tab("Steam Build Settings", EditorGuiLayoutHelper.GetIconTexture("steam"), () =>
                {
                    EditorGUILayout.LabelField("Steam Build Settings", EditorStylesHelper.TitleStyle);
                    EditorGUILayout.LabelField("<b>Steam Path:</b> " + _settings.steamPath, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Steam Path"))
                    {
                        _settings.steamPath = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }

                    if (GUILayout.Button("Open Folder"))
                    {
                        Process.Start(_settings.steamPath);
                    }

                    if (GUILayout.Button("Clear Folder"))
                    {
                        DirectoryInfo di = new DirectoryInfo(_settings.steamPath);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }
            
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("Publishing", EditorStylesHelper.TitleStyle);
                    
                    EditorGUILayout.LabelField("<b>Demo Publish Script Path:</b> " + _settings.steamDemoPublish, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Demo Publish Script Path"))
                    {
                        _settings.steamDemoPublish = EditorUtility.OpenFilePanel("Select script", "",  "bat");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }
                    if (GUILayout.Button("Run"))
                    {
                        Process.Start(_settings.steamDemoPublish);
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("<b>Beta Publish Script Path:</b> " + _settings.steamBetaPublish, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Beta Publish Script Path"))
                    {
                        _settings.steamBetaPublish = EditorUtility.OpenFilePanel("Select script", "",  "bat");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }
                    if (GUILayout.Button("Run"))
                    {
                        Process.Start(_settings.steamBetaPublish);
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("<b>Release Publish Script Path:</b> " + _settings.steamReleasePublish, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Release Publish Script Path"))
                    {
                        _settings.steamReleasePublish = EditorUtility.OpenFilePanel("Select script", "",  "bat");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }
                    if (GUILayout.Button("Run"))
                    {
                        Process.Start(_settings.steamReleasePublish);
                    }
                    EditorGUILayout.EndHorizontal();
                }),
                new Tab("Itch Build Settings", EditorGuiLayoutHelper.GetIconTexture("itch"), () =>
                {
                    EditorGUILayout.LabelField("Itch Build Settings", EditorStylesHelper.TitleStyle);
                    EditorGUILayout.LabelField("<b>Itch Path:</b> " + _settings.itchPath, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Itch Path"))
                    {
                        _settings.itchPath = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }

                    if (GUILayout.Button("Open Folder"))
                    {
                        Process.Start(_settings.itchPath);
                    }

                    if (GUILayout.Button("Clear Folder"))
                    {
                        DirectoryInfo di = new DirectoryInfo(_settings.itchPath);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("Publishing", EditorStylesHelper.TitleStyle);
                    
                    EditorGUILayout.LabelField("<b>Demo Publish Script Path:</b> " + _settings.itchDemoPublish, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Demo Publish Script Path"))
                    {
                        _settings.itchDemoPublish = EditorUtility.OpenFilePanel("Select script", "",  "bat");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }
                    if (GUILayout.Button("Run"))
                    {
                        Process.Start(_settings.itchDemoPublish);
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("<b>Beta Publish Script Path:</b> " + _settings.itchBetaPublish, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Beta Publish Script Path"))
                    {
                        _settings.itchBetaPublish = EditorUtility.OpenFilePanel("Select script", "",  "bat");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }
                    if (GUILayout.Button("Run"))
                    {
                        Process.Start(_settings.itchBetaPublish);
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("<b>Release Publish Script Path:</b> " + _settings.itchReleasePublish, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Release Publish Script Path"))
                    {
                        _settings.itchReleasePublish = EditorUtility.OpenFilePanel("Select script", "",  "bat");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }
                    if (GUILayout.Button("Run"))
                    {
                        Process.Start(_settings.itchReleasePublish);
                    }
                    EditorGUILayout.EndHorizontal();
                }),
                new Tab("Steam Deck Build Settings", EditorGuiLayoutHelper.GetIconTexture("Steam_Dev"), () =>
                {
                    EditorGUILayout.LabelField("Steam Deck Build Settings", EditorStylesHelper.TitleStyle);
                    EditorGUILayout.LabelField("<b>Steam Deck Build Path:</b> " + _settings.steamDeckPath, EditorStylesHelper.SubTitleStyle);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Steam Path"))
                    {
                        _settings.steamDeckPath = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }

                    if (GUILayout.Button("Open Folder"))
                    {
                        Process.Start(_settings.steamDeckPath);
                    }

                    if (GUILayout.Button("Clear Folder"))
                    {
                        DirectoryInfo di = new DirectoryInfo(_settings.steamDeckPath);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }
            
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.LabelField("Deployment", EditorStylesHelper.TitleStyle);
                    EditorGUILayout.LabelField("<b>Steam Deck App Path:</b> ", EditorStylesHelper.SubTitleStyle);
                    _settings.steamDeckClientApp = EditorGUILayout.TextField(_settings.steamDeckClientApp);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Save"))
                    {
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                    }

                    if (GUILayout.Button("Open App"))
                    {
                        Process.Start(_settings.steamDeckClientApp);
                    }
                    EditorGUILayout.EndHorizontal();
                }),
                new Tab("Build", EditorGuiLayoutHelper.GetIconTexture("Compile"), () =>
                {
                    EditorGUILayout.LabelField("Build Settings", EditorStylesHelper.TitleStyle);
            
                    _settings.demo = EditorGUILayout.Toggle("Demo Build",_settings.demo);
                    _settings.beta = EditorGUILayout.Toggle("Beta Build",_settings.beta);
                    _settings.itch = EditorGUILayout.Toggle("Itch Build", _settings.itch);
                    _settings.steamDeck = EditorGUILayout.Toggle("Steam Deck Build", _settings.steamDeck);
                    _settings.dev = EditorGUILayout.Toggle("Development Build", _settings.dev);

                    EditorGUILayout.Space(10.0f);

                    EditorGUILayout.LabelField("Build Targets", EditorStylesHelper.TitleStyle);

                    _settings.buildTargets.ForEach((x) =>
                    {
                        x.enabled = EditorGUILayout.Toggle(x.name, x.enabled);
                    });

                    if (GUILayout.Button("Build"))
                    {
                        BuildSettingsHelper.SaveBuildSettings(_settings);
                        _settings.buildTargets.Where(x => x.enabled).ToList().ForEach((x) =>
                        {
                            BuildGame(x.target, x.folder, x.extension);
                        });
                    }
                })
            });
        }
        
        void SaveProjectSettings()
        {
            if (!string.IsNullOrEmpty(_settings.ProjectName))
                PlayerSettings.productName = _settings.ProjectName;

            if (!string.IsNullOrEmpty(_settings.CompanyName))
                PlayerSettings.companyName = _settings.CompanyName;

            if (!string.IsNullOrEmpty(_settings.BaseBuildNumber))
            {
                PlayerSettings.bundleVersion = _settings.BaseBuildNumber;

                if (_settings.beta)
                {
                    PlayerSettings.bundleVersion += "." + _settings.BuildNumber;
                }
            }


            if (!string.IsNullOrEmpty(_settings.BundleIdentifier))
            {
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, _settings.BundleIdentifier);
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WebGL, _settings.BundleIdentifier);
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Switch, _settings.BundleIdentifier);
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.PS4, _settings.BundleIdentifier);
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, _settings.BundleIdentifier);
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.XboxOne, _settings.BundleIdentifier);
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WSA, _settings.BundleIdentifier);
            }

            if (_settings.BundleVersionCode > 0 && PlayerSettings.Android.bundleVersionCode < _settings.BundleVersionCode)
            {
                PlayerSettings.Android.bundleVersionCode = _settings.BundleVersionCode;
            }
            else
            {
                _settings.BundleVersionCode = PlayerSettings.Android.bundleVersionCode;
            }

            if (!string.IsNullOrEmpty(_settings.KeyStorePassword))
            {
                PlayerSettings.keystorePass = _settings.KeyStorePassword;
            }

            if (!string.IsNullOrEmpty(_settings.KeyAliasPassword))
            {
                PlayerSettings.keyaliasPass = _settings.KeyAliasPassword;
            }
            
            BuildSettingsHelper.SaveBuildSettings(_settings);
        }

        public void BuildGame(BuildTarget target, string folder, string extension)
        {
            BuildSettingsHelper.SaveBuildSettings(_settings);
            string inputPath = "", basePath = "";

            if (_settings.itch)
            {
                basePath = _settings.itchPath;
            }
            else if (_settings.steamDeck)
            {
                inputPath = _settings.steamDeckPath;
            }
            else if (target == BuildTarget.WSAPlayer)
            {
                inputPath = _settings.uwpPath;
            }
            else
            {
                basePath = _settings.steamPath;
            }

            if (!_settings.steamDeck && target != BuildTarget.WSAPlayer)
            {
                if (_settings.beta)
                {
                    inputPath = Path.Join(basePath, "beta");
                }
                else if (_settings.demo)
                {
                    inputPath = Path.Join(basePath, "demo");
                }
                else
                {
                    inputPath = Path.Join(basePath, "release");
                }
            }
            
            inputPath = Path.Join(inputPath, folder);
            
            if (!Directory.Exists(inputPath))
            {
                Directory.CreateDirectory(inputPath);
            }

            if (target != BuildTarget.WSAPlayer)
            {
                //Clear the folder
                DirectoryInfo di = new DirectoryInfo(inputPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            inputPath = Path.Join(inputPath, Application.productName + extension);
            
            List<string> defines = new List<string>() { "CROSS_PLATFORM_INPUT" };
            
            if (_settings.itch)
            {
                defines.Add("DISABLESTEAMWORKS");
                defines.Add("ITCH_BUILD");
            }

            if (target == BuildTarget.WSAPlayer)
            {
                defines.Add("DISABLESTEAMWORKS");
            }
            
            if (_settings.demo)
            {
                defines.Add("DEMO");
            }
            
            if (_settings.beta)
            {
                defines.Add("BETA");
            }
            
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, defines.ToArray());

            List<string> levels = new List<string>();

            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                levels.Add(scene.path);
            }

            // Build player.
            EditorPrefs.SetBool("BurstCompilation", false);

            BuildOptions options = _settings.dev ? BuildOptions.Development : BuildOptions.None;
            options |= BuildOptions.ShowBuiltPlayer; // Show the built player even if minimized

            var success = false;
            if (target == BuildTarget.WSAPlayer)
            {
                UwpBuildHelper.StartBuild(inputPath, levels.ToArray(), options);
            }
            else
            {
                var report = BuildPipeline.BuildPlayer(levels.ToArray(), inputPath, target, options);

                success = report.summary.result == BuildResult.Succeeded;
            }
            
            
            if (_settings.itch)
            {
                // Write the build number
                string buildNumber = PlayerSettings.bundleVersion;

                if (_settings.beta)
                {
                    buildNumber += "-beta";
                }

                string fileName = Path.Join(basePath, (_settings.beta) ? "betaBuildNumber.txt" : "buildNumber.txt");
                
                File.WriteAllText(fileName, buildNumber);
            }
            
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, new [] { "CROSS_PLATFORM_INPUT" });
        }
    }
}