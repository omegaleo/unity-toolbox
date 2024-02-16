using System;
using System.Collections;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Documentation;
using Object = UnityEngine.Object;

namespace OmegaLeo.Toolbox.Models
{
    [Documentation(nameof(BaseErrorReportingDestination), "Base class that is used to hold settings for destinations of the Custom Error Reporting hook.")]
    public class BaseErrorReportingDestination
    {
        [Documentation(nameof(DestinationName), "Identifier for the destination.")]
        public string DestinationName;

        [Documentation(nameof(SendAsync), "Override this method with the logic to send the log to the destination.")]
        public virtual IEnumerator SendAsync(Object message, LogType logType = LogType.Log, Exception e = null)
        {
            yield return null;
        }

        [Documentation(nameof(CreateSettingsFile), "Override this method with the logic to create the settings file for the log destination.")]
        public virtual void CreateSettingsFile()
        {
            
        }
    }
}