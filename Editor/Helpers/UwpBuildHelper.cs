using UnityEditor;

namespace OmegaLeo.Toolbox.Editor.Helpers
{
    public static class UwpBuildHelper
    {
        private const string UWP_SDK_VERSION = "10.0.22000.0";
        
        public static void StartBuild(string folder, string[] scenes, BuildOptions options)
        {
            BuildPlayerOptions opts = new BuildPlayerOptions
            {
                scenes = scenes,
                targetGroup = BuildTargetGroup.WSA,
                target = BuildTarget.WSAPlayer,
                locationPathName = folder,
                options = options
            };
            PlayerSettings.SetScriptingBackend(opts.targetGroup, ScriptingImplementation.IL2CPP);
            EditorUserBuildSettings.wsaUWPSDK = UWP_SDK_VERSION;

            BuildPipeline.BuildPlayer(opts);
        }
    }
}