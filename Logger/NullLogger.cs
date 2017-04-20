using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandREng.Utilities.Logging
{
    public class NullLogger : ILogger
    {
        public bool LoggingEnabled { get; set; }
        public EnLogLevel LogLevel { get; set; }
        public bool BufferErrors { get; set; }

        public void Log(EnLogLevel Level, string Message)
        {
        }

        public void Log(string Message)
        {
        }
        List<LogEntry> ILogger.GetAndClearErrors()
        {
            return null;
        }

        public void LogError(string Message)
        {
        }
        public void LogWarning(string Message)
        {
        }

        public void LogDebug(string Message)
        {
        }

        public void LogException(Exception ex, string message)
        {
        }


        public void LogException(Exception ex)
        {
        }

        public void LogSeparator()
        {
        }

        public void LogSeparator(char LineChar)
        {
        }

        public void LogSeparator(char LineChar, int Num)
        {
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~NullLogger() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
