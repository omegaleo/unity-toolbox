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
            string settingsPath = $"Assets/Build/BuildSettings.asset";

            var settings = ScriptableObject.CreateInstance<BuildSettings>();
            
            if (!Directory.Exists(Path.Join(Application.dataPath, "Build")))
            {
                Directory.CreateDirectory(Path.Join(Application.dataPath, "Build"));
            }

            if (!AssetDatabase.FindAssets(settingsPath).Any())
            {
                settings.ProjectName = PlayerSettings.productName;
                settings.CompanyName = PlayerSettings.companyName;
                settings.BundleIdentifier = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Standalone);
                settings.BundleVersionCode = PlayerSettings.Android.bundleVersionCode;
                settings.KeyStorePassword = PlayerSettings.keystorePass;
                settings.KeyAliasPassword = PlayerSettings.keyaliasPass;
                AssetDatabase.CreateAsset(settings, settingsPath);
            }

            settings = AssetDatabase.LoadAssetAtPath<BuildSettings>(settingsPath);
            EditorUtility.SetDirty(settings);
            return settings;
        }
        
        public static void SaveBuildSettings(BuildSettings settings)
        {
            AssetDatabase.SaveAssets();
        }
    }
}