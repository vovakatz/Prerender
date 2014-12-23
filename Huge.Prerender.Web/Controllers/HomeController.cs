using Huge.Prerender.Models.Scheduler;
using Huge.Prerenderer.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Huge.Prerender.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            JobManager jobManager = new JobManager();

            JobDetail jobDetail = new JobDetail();

            jobDetail.CronExpression = "0/5 * * * * ?";
            jobDetail.Description = "test";
            jobDetail.Name = "test";

            jobManager.CreateJob(jobDetail);

            return View();
        }
    }
}