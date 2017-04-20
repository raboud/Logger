using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace RandREng.Utilities.Logging
{
    public sealed class Logger
    {

        private Logger()
        {
        }

#if !NETCORE

        public static void WriteEventLog(string Source, Exception ex)
        {
            WriteEventLog(Source, LogEntry.GetException(ex));
        }

        public static void WriteEventLog(string Source, string Message)
        {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(Source))
                {
                    System.Diagnostics.EventLog.CreateEventSource(Source, "Application");
                }

                EventLog log = new EventLog();
                log.Source = Source;
                log.WriteEntry(Message);
            }
            catch(Exception )
            {
            }
        }
#endif

    }
}
