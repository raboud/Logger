using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandREng.Utilities.Logging
{
    public class EmailLogger : QueueLogger
    {


        public EmailLogger()
        {

        }

        protected override void Flush()
        {
            throw new NotImplementedException();
        }

        protected override Mutex getMutex()
        {
            throw new NotImplementedException();
        }
    }
}
