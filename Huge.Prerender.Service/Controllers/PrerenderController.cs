using Huge.Prerender.Core.DataService;
using Huge.Prerender.Models.Scheduler;
using System.Web.Http;

namespace Huge.Prerender.Service.Controllers
{
    public class PrerenderController : ApiController
    {
        public void Post([FromBody]JobData jobData)
        {
            IDataService dataService = GetDataService(jobData.StorageType);
            Core.Prerender prerender = new Core.Prerender(dataService);
            prerender.ProcessSite(jobData.Key, jobData.sitemapUrl);
        }

        public string Get()
        { 
            return "Executed Get";
        }

        private IDataService GetDataService(Models.Enums.StorageType storageType)
        {
            IDataService dataService;
            switch (storageType)
            {
                case Models.Enums.StorageType.File:
                    dataService = new FileDataService();
                    break;
                case Models.Enums.StorageType.MongoDB:
                    dataService = new MongoDataService();
                    break;
                default:
                    dataService = new FileDataService();
                    break;
            }
            return dataService;
        }
    }
}
