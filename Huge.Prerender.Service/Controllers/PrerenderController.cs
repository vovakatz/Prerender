using Huge.Prerender.Core.DataService;
using Huge.Prerender.Models.Scheduler;
using System.Web.Http;

namespace Huge.Prerender.Service.Controllers
{
    public class PrerenderController : ApiController
    {
        [HttpPost]
        public void Start([FromBody]JobData jobData)
        {
            IDataService dataService = DataServiceFactory.GetDataService(jobData.StorageType);
            Core.Prerender prerender = new Core.Prerender(dataService);
            prerender.ProcessSite(jobData.Key, jobData.sitemapUrl);
        }

        [HttpPost]
        public void Stop()
        {
            Core.Prerender.Stop = true;
        }

        public string Get()
        { 
            return "Executed Get";
        }
    }
}
