using Huge.Prerender.Models.Scheduler;
using Huge.Prerenderer.Scheduler.Jobs;
using Quartz;

namespace Huge.Prerenderer.Scheduler
{
    public class JobManager
    {
        public void CreateJob(JobDetail jobDetail)
        {
            IJobDetail job = JobBuilder.Create().OfType(typeof(PrerenderJob)).WithIdentity(jobDetail.Name).StoreDurably(true).WithDescription(jobDetail.Description).Build();
            ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(jobDetail.CronExpression).WithDescription(jobDetail.Description).Build();

            var scheduler = QuartzScheduler.Instance;
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
        }

        public void testJob()
        {
            JobDetail jobDetail = new JobDetail();

            jobDetail.CronExpression = "0/5 * * * * ?";
            jobDetail.Description = "test";
            jobDetail.Name = "test";

            CreateJob(jobDetail);
        }
    }
}
