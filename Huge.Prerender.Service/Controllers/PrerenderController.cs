using Huge.Prerender.Core.DataService;
using Huge.Prerender.Models.Scheduler;
using System.Web.Http;

namespace Huge.Prerender.Service.Controllers
{
    public class PrerenderController : ApiController
    {
        public void Post([FromBody]JobData jobData)
        {
            IDataService persister;
            switch (jobData.StorageType)
            {
                case Models.Enums.StorageType.File:
                    persister = new FileDataService();
                    break;
                case Models.Enums.StorageType.MongoDB:
                    persister = new MongoDataService();
                    break;
                default:
                    persister = new FileDataService();
                    break;
            }
            Core.Prerender prerender = new Core.Prerender(persister);
            prerender.ProcessSite(jobData.Key, jobData.sitemapUrl);
        }

        public string Get()
        { 
            return "Executed Get";
        }
    }
}
