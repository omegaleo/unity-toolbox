using System.IO;
using System.Linq;
using Omega_Leo_Toolbox.Editor.Models;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Helpers
{
    public static class BuildSettingsHelper
    {
        public static BuildSettings GetOrCreateBuildSettings()
        {
            string settingsPath = "Assets/Build/BuildSettings.asset";


            if (!Directory.Exists(Path.Join(Application.dataPath, "Build")))
            {
                Directory.CreateDirectory(Path.Join(Application.dataPath, "Build"));
            }

            var settings = AssetDatabase.LoadAssetAtPath<BuildSettings>(settingsPath);
            
            if (settings == null)
            {
                Debug.Log("Didn't find a BuildSettings file, creating one...");
                settings = ScriptableObject.CreateInstance<BuildSettings>();
                settings.ProjectName = PlayerSettings.productName;
                settings.CompanyName = PlayerSettings.companyName;
                settings.BundleIdentifier = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Standalone);
                settings.BundleVersionCode = PlayerSettings.Android.bundleVersionCode;
                settings.KeyStorePassword = PlayerSettings.keystorePass;
                settings.KeyAliasPassword = PlayerSettings.keyaliasPass;
                AssetDatabase.CreateAsset(settings, settingsPath);
            }

            return settings;
        }
        
        public static void SaveBuildSettings(BuildSettings settings)
        {
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }
    }
}