using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandREng.Utilities.Logging
{
    public class ConsoleLogger : BaseLogger
    {
        Process p;
        StreamWriter sw;

        public ConsoleLogger() : base()
        {

            ProcessStartInfo psi = new ProcessStartInfo("more.com")
            {
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            p = Process.Start(psi);

            sw = p.StandardInput;


            sw.WriteLine("Hello world!");
            this.LoggingEnabled = true;
        }

        override protected bool LogReady
        {
            get
            {
                return (true);
            }
        }

        override protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    sw.Dispose();
                    p.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }


        override protected void WriteLog(LogEntry entry)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            sw.Write(entry.ToString());
        }
    }
}
