using System;
using System.Collections.Generic;
namespace RandREng.Utilities.Logging
{
    public enum EnLogLevel { DEBUG = 0, INFO = 1, WARNING = 2, ERROR = 3, EXCEPTION = 4, HEARTBEAT = 5 };

    public interface ILogger : IDisposable
    {
#region Properties
        bool LoggingEnabled { get; set; }
        EnLogLevel LogLevel { get; set; }
#endregion

        void Log(EnLogLevel Level, string Message);
        void Log(string Message);
        void LogError(string Message);
        void LogWarning(string Message);
        void LogDebug(string Message);
        void LogException(Exception ex, string message);
        void LogException(Exception ex);
        void LogSeparator();
        void LogSeparator(char LineChar);
        void LogSeparator(char LineChar, int Num);

    }
}
