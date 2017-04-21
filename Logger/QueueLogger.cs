using System;
using System.Threading;
using System.Collections.Concurrent;

namespace RandREng.Utilities.Logging
{
    abstract public class QueueLogger : BaseLogger
    {
        protected ConcurrentQueue<LogEntry> _queue = new ConcurrentQueue<LogEntry>();
        protected System.Timers.Timer _timer;

        override protected void WriteLog(LogEntry entry)
        {
            this._queue.Enqueue(entry);
        }
        abstract protected Mutex getMutex();
        abstract protected void Flush();

        protected void startTimer()
        {
            this._timer = new System.Timers.Timer(30000);
            this._timer.Elapsed += _timer_Elapsed;
            this._timer.Enabled = true;
            this._timer.AutoReset = true;
            this._timer.Start();
        }

        virtual protected void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            using (Mutex mutex = getMutex())
            {
                if (mutex.WaitOne(0))
                {
                    try
                    {
                        Flush();

                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }

                }
            }
        }

        override protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer.Enabled = false;

                    using (Mutex mutex = getMutex())
                    {
                        if (mutex.WaitOne())
                        {
                            // TODO: we should be waiting here in case the callback is already running.
                            try
                            {
                                Flush();
                            }
                            finally
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                    _timer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }



    }
}
