using System.IO;
using Omega_Leo_Toolbox.Editor.Models;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Helpers
{
    public static class BuildSettingsHelper
    {
        public static BuildSettings GetBuildSettings()
        {
            string settingsPath = $"Assets/Build/BuildSettings.asset";

            if (!Directory.Exists(Path.Join(Application.dataPath, "Build")))
            {
                Directory.CreateDirectory(Path.Join(Application.dataPath, "Build"));
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BuildSettings>(), settingsPath);
            }

            return AssetDatabase.LoadAssetAtPath<BuildSettings>(settingsPath);
        }
        
        public static void SaveBuildSettings(BuildSettings settings)
        {
            string settingsPath = $"Assets/Build/BuildSettings.asset";

            if (!Directory.Exists(Path.Join(Application.dataPath, "Build")))
            {
                Directory.CreateDirectory(Path.Join(Application.dataPath, "Build"));
                AssetDatabase.CreateAsset(settings, settingsPath);
            }
            
            EditorUtility.SetDirty(settings);
            
            AssetDatabase.SaveAssets();
        }
    }
}