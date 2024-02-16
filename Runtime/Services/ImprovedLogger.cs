using System;
using System.Collections.Generic;
using OmegaLeo.Toolbox.Models;
using OmegaLeo.Toolbox.Runtime.Models;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Documentation;
using Object = UnityEngine.Object;

namespace OmegaLeo.Toolbox.Runtime.Services
{
    [Documentation(nameof(ImprovedLogger), @"Improved version of the Unity logging system that allows the developer to write their own log destinations via the " + nameof(BaseErrorReportingDestination) + " class as a extension class. Use cases for this logger include collecting certain logs in development builds.")]
    public class ImprovedLogger : ILogHandler
    {
        private List<BaseErrorReportingDestination> _destinations;
        private ILogHandler _originalLogger;
        private LogReport _report;
        private LogType _minimumLogLevel;
        private MonoBehaviour _monoBehaviour;
        
        public static ImprovedLogger Register(LogType minimumLogLevel, MonoBehaviour monoBehaviour, List<BaseErrorReportingDestination> destinations,bool developmentBuildOnly = true)
        {
            if (developmentBuildOnly && !Debug.isDebugBuild)
            {
                return null;
            }
            
            var logger = new ImprovedLogger(Debug.unityLogger.logHandler, minimumLogLevel, monoBehaviour, destinations);
            Debug.unityLogger.logHandler = logger;

            return logger;
        }

        public ImprovedLogger(ILogHandler originalLogger, LogType minimumLogLevel, MonoBehaviour monoBehaviour,
            List<BaseErrorReportingDestination> destinations)
        {
            _originalLogger = originalLogger;
            _minimumLogLevel = minimumLogLevel;
            _monoBehaviour = monoBehaviour;
            _destinations = destinations;
        }

        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            Log(context, logType, null, args);
            _originalLogger.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, Object context)
        {
            Log(context, LogType.Exception, exception, Array.Empty<object>());
            _originalLogger.LogException(exception, context);
        }
        
        public void Log(Object obj, LogType type, Exception ex, object[] args)
        {
            if (!Application.isPlaying) return;

            string content = (ex != null) ? ex.Message + Environment.NewLine + ex.StackTrace : (obj != null) ? obj.ToString() : "";

            foreach (object o in args)
            {
                content += o;
            }

            _report.Logs.Add(content);

            if (content.Contains("InputSystem", StringComparison.OrdinalIgnoreCase)) return;

            if (type >= _minimumLogLevel)
            {
                // Send to the various destinations
                foreach (var dest in _destinations)
                {
                    _monoBehaviour.StartCoroutine(dest.SendAsync(obj, type, ex));
                }
            }
        }
    }
}