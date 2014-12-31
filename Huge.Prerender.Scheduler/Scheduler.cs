using Huge.Prerender.Models.Scheduler;
using RestSharp;
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
            //System.Diagnostics.Debugger.Launch();
            //read configuration
            SchedulerConfigurationSection config = (SchedulerConfigurationSection)ConfigurationManager.GetSection("schedulerSection");
            
            //start quartz scheduler
            for (int i = 0; i < config.SchedulerSettings.Count; i++)
            {
                JobData jobData = new JobData();
                jobData.ServiceEndPointUrl = config.SchedulerSettings[i].ServiceEndPointUrl;
                jobData.sitemapUrl = config.SchedulerSettings[i].SitemapUrl;
                jobData.Key = config.SchedulerSettings[i].WebsiteKey;
                jobData.StorageType = config.SchedulerSettings[i].StorageType;

                JobManager manager = new JobManager();
                manager.CreateJob(config.SchedulerSettings[i], jobData);
            }
        }

        protected override void OnStop()
        {
            //stop scheduler
            var scheduler = QuartzScheduler.Instance;
            scheduler.Shutdown();

            //stop renderer
            var client = new RestClient("http://localhost/Huge.Prerender.Service/api");
            var request = new RestRequest("Prerender/Stop", Method.POST);
            client.Execute(request);
        }
    }
}
