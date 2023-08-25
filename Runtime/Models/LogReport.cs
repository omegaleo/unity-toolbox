using System.Collections.Generic;

namespace OmegaLeo.Toolbox.Runtime.Models
{
    public class LogReport
    {
        public string BuildVersion;
        public List<string> Logs;

        public LogReport(string buildVersion)
        {
            BuildVersion = buildVersion;
            Logs = new List<string>();
        }
    }
}