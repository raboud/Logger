using System;
using System.IO;
using System.Text;
using System.Threading;

namespace RandREng.Utilities.Logging
{
    public class FileLogger : QueueLogger
    {
        private string m_LogPathName = "";
        private string m_LogBaseFileName = "";
        private bool m_LogFileSet = false;

#if !NETCORE
        public FileLogger()
            : this(Properties.Settings.Default.LogLocation, Properties.Settings.Default.LogFilename)
        {
            this.LogLevel = (EnLogLevel)Enum.Parse(typeof(EnLogLevel), Properties.Settings.Default.LogLevel.ToUpper());
        }
#endif

        public FileLogger(string LogPathName, string LogBaseFileName)
        {
            LoggingEnabled = true;
            LogLevel = EnLogLevel.INFO;

            lock (syncRoot)
            {
                if (false == LogPathName.EndsWith("\\"))
                {
                    LogPathName += @"\";
                }

                m_LogPathName = LogPathName;
                m_LogBaseFileName = LogBaseFileName;

                // if path does not exist, create it.
                if (!Directory.Exists(m_LogPathName))
                {
                    Directory.CreateDirectory(m_LogPathName);
                }

                m_LogFileSet = true;
            }
            startTimer();
        }



        override protected async void Flush()
        {
            int count = _queue.Count;
            if (!Directory.Exists(Path.GetDirectoryName(LogFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(LogFile));

            using (StreamWriter sw = File.AppendText(LogFile))
            {
                StringBuilder sb = new StringBuilder();
                while (count > 0)
                {
                    if (_queue.TryDequeue(out LogEntry entry))
                    {
                        sb.Append(entry.ToString());
                    }
                    --count;
                }
                await sw.WriteAsync(sb.ToString());
                sb.Clear();
#if !NETCORE
                sw.Close();
#endif
                sw.Dispose();

            }
        }

        override protected bool LogReady
        {
            get
            {
                return (m_LogFileSet);
            }
        }

        private string LogFile;
        private string lastname = "";
        private string mutexname = "";

        override protected Mutex getMutex()
        {
            return new Mutex(false, getMutexName());
        }
        
        private string getMutexName()
        {
            LogFile = GetFileName();
            if (lastname != LogFile)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(LogFile);
                mutexname = Convert.ToBase64String(bytes);
                this.lastname = LogFile;
            }
            return mutexname;
        }

        public string GetFileName()
        {
            string DateString = DateTime.Now.ToShortDateString();
            DateString = DateString.Replace("/", "_");
            string LogFile = m_LogPathName + m_LogBaseFileName + "_" + DateString + ".log";
            return LogFile;
        }



    }

}
