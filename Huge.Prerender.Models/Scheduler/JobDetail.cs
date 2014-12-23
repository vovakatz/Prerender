
namespace Huge.Prerender.Models.Scheduler
{
    public class JobDetail
    {
        public string Name { get; set; }
        public string CronExpression { get; set; }
        public string ServiceEndPointUrl { get; set; }
        public string UrlRequestType { get; set; }
        public string Description { get; set; }
    }
}
