using Huge.Prerender.Models.Scheduler;
using Huge.Prerender.Scheduler.Jobs;
using Quartz;

namespace Huge.Prerender.Web
{
    public class JobManager
    {
        public void CreateJob(JobData jobDetail)
        {
            IJobDetail job = JobBuilder.Create<PrerenderJob>().WithIdentity(jobDetail.Name).StoreDurably(true).WithDescription(jobDetail.Description).Build();
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(jobDetail.CronExpression).WithDescription(jobDetail.Description).Build();
            job.JobDataMap.Put("yoyo", "test");
            var scheduler = QuartzScheduler.Instance;
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
        }

        public void testJob()
        {
            JobData jobDetail = new JobData();

            jobDetail.CronExpression = "0/5 * * * * ?";
            jobDetail.Description = "test";
            jobDetail.Name = "test";

            CreateJob(jobDetail);
        }
    }
}
