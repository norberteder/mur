using System;
using System.Collections.Generic;
using System.Timers;
namespace Mur
{
    public class Scheduler
    {
        private Timer timer;
        private Dictionary<string, JobBase> jobs = new Dictionary<string, JobBase>();

        private List<JobSettings> JobSettings { get; set; }

        private void Initialize(List<JobSettings> settings)
        {
            timer = new Timer(60000);
            timer.Elapsed += Tick;

            JobSettings = settings;

            foreach (var jobSetting in JobSettings)
            {
                var job = Activator.CreateInstance(Type.GetType(jobSetting.Type)) as JobBase;
                jobs.Add(jobSetting.Id, job);
            }
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            // find and execute jobs
        }

        public void Start(List<JobSettings> jobSettings)
        {
            Initialize(jobSettings);

            timer.Start();
        }

        public void ExecuteJob(JobBase job)
        {
            if (job == null)
            {
                throw new ArgumentNullException("job");
            }

            job.Run();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Restart(List<JobSettings> jobSettings)
        {
            Stop();
            Start(jobSettings);
        }
    }
}
