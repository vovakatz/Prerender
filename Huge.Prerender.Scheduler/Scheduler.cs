using Huge.Prerender.Models.Scheduler;
using System.Configuration;
using System.ServiceProcess;

namespace Huge.Prerender.Scheduler
{
    public partial class Scheduler : ServiceBase
    {
        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            //read configuration
            SchedulerConfigurationSection config = (SchedulerConfigurationSection)ConfigurationManager.GetSection("schedulerSection");
            
            //start quartz scheduler
            for (int i = 0; i < config.SchedulerSettings.Count; i++)
            {
                JobData jobData = new JobData();
                jobData.ServiceEndPointUrl = config.SchedulerSettings[i].ServiceEndPointUrl;
                jobData.sitemapUrl = config.SchedulerSettings[i].SitemapUrl;

                JobManager manager = new JobManager();
                manager.CreateJob(config.SchedulerSettings[i], jobData);
            }
        }

        protected override void OnStop()
        {
            var scheduler = QuartzScheduler.Instance;
            scheduler.Shutdown();
        }
    }
}
