using Huge.Prerender.Models.Scheduler;
using System.Web.Mvc;

namespace Huge.Prerender.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            JobManager jobManager = new JobManager();

            JobData jobData = new JobData();

            jobData.CronExpression = "0/5 * * * * ?";
            jobData.Description = "test";
            jobData.Name = "test";

            jobManager.CreateJob(jobData);

            return View();

            //SchedulerConfigurationSection config = (SchedulerConfigurationSection)ConfigurationManager.GetSection("schedulerSection");

            return View();
        }
    }
}