using System.Collections.Generic;
using OmegaLeo.Toolbox.Attributes;
using UnityEditor;
using UnityEngine;

namespace Omega_Leo_Toolbox.Editor.Models
{
    public class BuildSettings : ScriptableObject
    {
        [ColoredHeader("Paths")] 
        
        public string itchPath = "";
        public string steamPath = "";
        public string steamDeckPath = "";
        public string uwpPath = "";

        [ColoredHeader("Upload Scripts")] public string steamDeckClientApp = "steam://rungameid/943760";

        public string steamReleasePublish = "";
        public string steamDemoPublish = "";
        public string steamBetaPublish = "";

        public string itchReleasePublish = "";
        public string itchDemoPublish = "";
        public string itchBetaPublish = "";

        [ColoredHeader("Build Flags")] public bool demo = false;
        
        public bool beta = false;
        public bool itch = false;
        public bool steamDeck = false;
        public bool dev = false;

        public List<SceneAsset> demoScenes = new List<SceneAsset>();

        public List<SceneAsset> releaseScenes = new List<SceneAsset>();

        [ColoredHeader("Build Settings")] public string ProjectName;

        public string CompanyName;
        public string CompanyNameForUWP; //Because it can't have ""
        public string BaseBuildNumber;

        public string BundleIdentifier;
        public int BundleVersionCode;

        public string KeyAliasPassword;

        public string KeyStorePassword;

        public int BuildNumber = 0;

        public List<CustomBuildTarget> buildTargets = new List<CustomBuildTarget>()
        {
            new CustomBuildTarget()
                {enabled = true, folder = "win", extension = ".exe", target = BuildTarget.StandaloneWindows64, name = "Windows"},
            new CustomBuildTarget()
                {enabled = true, folder = "linux", extension = ".x86_64", target = BuildTarget.StandaloneLinux64, name = "Linux"},
            new CustomBuildTarget()
                {enabled = true, folder = "android", extension = ".apk", target = BuildTarget.Android, name = "Android"},
            new CustomBuildTarget()
                {enabled = true, folder = "osx", extension = "", target = BuildTarget.StandaloneOSX, name = "Mac OS X" },
            new CustomBuildTarget() {enabled = true, folder = "webgl", extension = "", target = BuildTarget.WebGL, name = "WebGL" },
            new CustomBuildTarget() {enabled = true, folder = "uwp", extension = "", target = BuildTarget.WSAPlayer, name = "UWP" },
            new CustomBuildTarget()
                {enabled = true, folder = "steamdeck", extension = ".x86_64", target = BuildTarget.StandaloneLinux64, name = "SteamDeck" }
        };
    }
}