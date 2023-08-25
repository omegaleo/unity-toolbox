using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Models
{
    [CreateAssetMenu(fileName = "LogDestinationSettings", menuName = "Omega Leo/Log Destination Settings")]
    public class LogDestinationSettings : ScriptableObject
    {
        public string Name = "";
        
        public List<LogDestinationSetting> Settings = new List<LogDestinationSetting>();

        public LogDestinationSettings(string name)
        {
            Name = name;
        }

        public LogDestinationSettings()
        {
        }

        public bool HasValue(string key) => Settings.Any(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        
        public string GetValue(string key) => Settings.FirstOrDefault(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase))?.Value ?? "";

        public void SetValue(string key, string value)
        {
            if (HasValue(key))
            {
                Settings.FirstOrDefault(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase))!.Value = value;
            }
            else
            {
                Settings.Add(new LogDestinationSetting(key, value));
            }
        }
    }

    public class LogDestinationSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public LogDestinationSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}