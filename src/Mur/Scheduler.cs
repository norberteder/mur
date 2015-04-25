using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;

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
                var jobType = Type.GetType(jobSetting.Type);
                var job = Activator.CreateInstance(jobType) as JobBase;
                jobs.Add(jobSetting.Id, job);
            }
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;

            // find jobr
            var jobsSettingsToRun = JobSettings.Where(jobSetting => jobSetting.Schedules != null && jobSetting.Schedules.Length > 0)
                .Where(jobSetting => jobSetting.Schedules.Min(s => SchedulerHelper.GetNextRun(s.Start, s.Interval, jobs[jobSetting.Id].LastRun.HasValue ? jobs[jobSetting.Id].LastRun.Value : DateTime.MinValue) <= now))
                .ToList();

            // is job currently running?

            // and execute jobs

            foreach (var jobSetting in jobsSettingsToRun)
            {
                var job = jobs[jobSetting.Id];
                
                var task = new Task(job.Run).ContinueWith((result) =>
                {
                    UpdateLastRun(job, now);
                });

                task.Start();
            }
        }

        private void UpdateLastRun(JobBase job, DateTime lastRun)
        {
            job.LastRun = lastRun;

            // trigger event for handling storage/messaging/whatever
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
