using System;
using UnityEditor;

namespace Omega_Leo_Toolbox.Editor.Models
{
    [Serializable]
    public class CustomBuildTarget
    {
        public string name;
        public BuildTarget target;
        public string folder;
        public string extension;
        public bool enabled;
    }
}