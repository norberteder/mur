using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mur.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var exptectedJobId = "FibonacciJob";

            var schedule = new Schedule()
            {
                Start = DateTime.Now,
                Interval = "1 Minute"
            };

            var jobSettingsList = new List<JobSettings> {
                new JobSettings {
                    Id = exptectedJobId,
                    Type = typeof(FibonacciJob).AssemblyQualifiedName,
                    Schedules = new [] {
                        schedule
                    }
                }
            };

            var scheduler = new Scheduler();
            scheduler.JobFinished += (sender, arg) =>
            {
                if (arg.JobId == exptectedJobId)
                {
                    Console.WriteLine("Job '{0}' executed", arg.JobId);
                }
            };

            scheduler.Start(jobSettingsList);

            Console.ReadLine();

        }
    }
}
