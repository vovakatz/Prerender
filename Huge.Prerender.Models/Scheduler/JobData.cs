
namespace Huge.Prerender.Models.Scheduler
{
    public class JobData
    {
        public string Key { get; set; }
        public string ServiceEndPointUrl { get; set; }
        public string ServiceRequestType { get; set; }
        public string sitemapUrl { get; set; }
        public Enums.StorageType StorageType { get; set; }
    }
}
