using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RandREng.Utilities.Logging
{
    public class BaseLogger : ILogger
    {
        public EnLogLevel LogLevel { get; set; }
        public bool LoggingEnabled { get; set; }
        protected object syncRoot = new Object();
        
        public BaseLogger()
        {
            this.BufferErrors = false;
        }
        public void Log(string Message)
        {
            Log(EnLogLevel.INFO, Message, null);
        }

        public void Log(EnLogLevel Level, string Message)
        {
            Log(Level, Message, null);
        }

        public void LogError(string Message)
        {
            Log(EnLogLevel.ERROR, Message);
        }


        public void LogWarning(string Message)
        {
            Log(EnLogLevel.WARNING, Message, null);
        }

        public void LogDebug(string Message)
        {
            Log(EnLogLevel.DEBUG, Message, null);
        }

        public void LogException(Exception ex, string message)
        {
            Log(EnLogLevel.EXCEPTION, message, ex);
        }

        public void LogException(Exception ex)
        {
            Log(EnLogLevel.EXCEPTION, null, ex);
        }

        public void LogSeparator()
        {
            LogSeparator('=');
        }

        public void LogSeparator(char LineChar)
        {
            LogSeparator(LineChar, 65);
        }

        virtual public void LogSeparator(char LineChar, int Num)
        {
            Log(LineChar, Num);
        }

        protected List<LogEntry> Errors
        {
            get
            {
                if (m_Errors == null)
                {
                    m_Errors = new List<LogEntry>();
                }
                return m_Errors;
            }
        }

        virtual protected bool LogReady
        {
            get
            {
                return false;
            }
        }

        public void Log(EnLogLevel Level, string Message, Exception ex = null)
        {
            if (LoggingEnabled)
            {

                lock (syncRoot)
                {
                    if (LogReady)
                    {
                        if (Level >= LogLevel)
                        {
                            LogEntry entry = new LogEntry(Level, Message, ex);
                            WriteLog(entry);
                        }
                    }
                }
            }
        }

        public void Log(char LineChar, int Num)
        {
            if (LoggingEnabled)
            {

                lock (syncRoot)
                {
                    if (LogReady)
                    {
                        LogEntry entry = new LogEntry(LineChar, Num);
                        WriteLog(entry);
                    }
                }
            }
        }

        protected List<LogEntry> m_Errors;

        public bool BufferErrors { get; set; }

        public List<LogEntry> GetAndClearErrors()
        {
            List<LogEntry> TempErrors = new List<LogEntry>();
            lock (syncRoot)
            {
                TempErrors.AddRange(this.Errors);
                this.Errors.Clear();
            }
            return TempErrors;
        }

        virtual protected void WriteLog(LogEntry entry)
        {
        }


        protected void BufferError(LogEntry line)
        {
            // if this is a warning or worse, add it to our errors list so we can email it later
            if (BufferErrors && (line.Level == EnLogLevel.ERROR || line.Level == EnLogLevel.EXCEPTION))
            {
                Errors.Add(line);
            }
        }

        #region IDisposable Support
        protected bool disposedValue = false; // To detect redundant calls

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
        // ~BaseLogger() {
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
