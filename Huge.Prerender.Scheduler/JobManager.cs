using Huge.Prerender.Models.Scheduler;
using Huge.Prerender.Scheduler.Jobs;
using Quartz;

namespace Huge.Prerender.Scheduler
{
    public class JobManager
    {
        public void CreateJob(SchedulerSettingElements config, JobData jobData)
        {
            IJobDetail job = JobBuilder.Create<PrerenderJob>()
                .WithIdentity(config.WebsiteKey)
                .StoreDurably(true).Build();
            job.JobDataMap.Put("info", jobData);
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(config.CronExpression).Build();

            var scheduler = QuartzScheduler.Instance;

            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
        }
    }
}
