using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mur
{
    public class JobFinishedEventArgs : EventArgs
    {
        public string JobId { get; private set; }
        public DateTime LastRun { get; private set; }

        public JobFinishedEventArgs(string jobId, DateTime lastRun)
        {
            JobId = jobId;
            LastRun = lastRun;
        }
    }
}
