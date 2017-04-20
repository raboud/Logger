using RandREng.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp1
{
    class Program
    {
        private static Timer aTimer;
        static ILogger logger;

        static void Main(string[] args)
        {
//            using (logger = new FileLogger(@"c:\temp\", "test"))
            using (logger = new ConsoleLogger())
            {
                // Create a timer and set a two second interval.
                aTimer = new System.Timers.Timer();
                aTimer.Interval = 2000;

                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;

                // Have the timer fire repeated events (true is the default)
                aTimer.AutoReset = true;

                // Start the timer
                aTimer.Enabled = true;

                Console.WriteLine("Press the Enter key to exit the program at any time... ");
                Console.ReadLine();
            }
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Exception ex = new NullReferenceException("This is Test", new Exception("INNER"));
            logger.LogSeparator();
            logger.Log(EnLogLevel.INFO, "Test1");
            logger.Log(EnLogLevel.INFO, "Test2");
            logger.Log(EnLogLevel.INFO, "Test3");
            logger.Log(EnLogLevel.INFO, "Test4");
            logger.LogException(ex);
            logger.LogException(ex, "Test 6");
            try
            {
                ExceptionTest1();
            }
            catch (Exception ex1)
            {
                logger.LogException(ex1);
            }
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }

        private static void ExceptionTest1()
        {
            try
            {
                ExceptionTest2();
            }
            catch (Exception ex1)
            {
                logger.LogException(ex1);
                throw new NullReferenceException("Woops", ex1);
            }

        }
        private static void ExceptionTest2()
        {
            throw new InvalidOperationException("ExceptionTest2");
        }


    }
}
