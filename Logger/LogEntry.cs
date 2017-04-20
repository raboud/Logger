using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandREng.Utilities.Logging
{
    public class LogEntry
    {
        public DateTime? Timestamp { get; private set; }
        public EnLogLevel Level { get; set; }
        public string Message { get; private set; }
        public Exception Exception { get; set; }
        public bool Seperator { get; set; }
        public char LineChar { get; set; }
        public int NumChar { get; set; }
        static public bool TimeStampOutput { get; set; } = true;
        static public string TimeStampFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fffzzz";

        private const int LOG_TYPE_COL_WIDTH = 13;
        private const int LOG_SEPERATOR_WIDTH = 65;


        public LogEntry(EnLogLevel level, string message, Exception ex = null)
        {
            this.Level = level;
            this.Timestamp = DateTime.UtcNow;
            this.Message = message;
            this.Seperator = false;
            this.Exception = ex;
        }

        public LogEntry(char lineChar, int num = LOG_SEPERATOR_WIDTH)
        {
            this.Timestamp = null;
            this.Message = null;
            this.Exception = null;
            this.Seperator = true;
            this.LineChar = lineChar;
            this.NumChar = num;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.Seperator)
            {
                AppendSeperator(builder);            }
            else
            {
                AppendTimestamp(builder);
                AppendLevel(builder);
                AppendMessage(builder);
                AppendException(builder);
            }
            return builder.ToString();
        }

        public void AppendSeperator(StringBuilder builder)
        {
            builder.AppendLine("".PadRight(this.NumChar, LineChar));
        }

        static public string GetException(Exception ex)
        {
            // wrap in try catch so we don't let logging error bring it all down
            StringBuilder sb = new StringBuilder();
            Exception innerException = ex;
            string prefix = "";
            while (innerException != null)
            {
                if (innerException.StackTrace != null)
                {
                    sb.AppendFormat("{0}{1}\r\n{2}\r\n", prefix, innerException.Message, innerException.StackTrace);
                }
                else
                {
                    sb.AppendFormat("{0}{1}\r\n", prefix, innerException.Message);
                }
                innerException = innerException.InnerException;
                prefix = "[INNER EXCEPTION] ";
            }

            return sb.ToString();
        }

        private void AppendTimestamp(StringBuilder builder)
        {
            if (TimeStampOutput && Timestamp.HasValue)
            {
                builder.AppendFormat("{0}  ", Timestamp.Value.ToLocalTime().ToString(TimeStampFormat));
            }

        }

        private void AppendLevel(StringBuilder builder)
        {
            string LevelString = "[" + Level.ToString() + "]";
            LevelString = LevelString.PadRight(LOG_TYPE_COL_WIDTH, ' ');
            builder.Append(LevelString);
        }

        private void AppendMessage(StringBuilder builder)
        {
            if (!string.IsNullOrEmpty(this.Message))
            {
                builder.AppendLine(Message);
            }
        }

        private void AppendException(StringBuilder builder)
        {
            if (Exception != null)
            {
                builder.Append(GetException(this.Exception));
            }
        }
    }
}
