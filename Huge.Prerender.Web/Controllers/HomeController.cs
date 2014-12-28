using System.Web.Mvc;

namespace Huge.Prerender.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //SchedulerConfigurationSection config = (SchedulerConfigurationSection)ConfigurationManager.GetSection("schedulerSection");

            return View();
        }
    }
}