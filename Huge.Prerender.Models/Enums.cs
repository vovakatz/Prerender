
namespace Huge.Prerender.Models
{
    public class Enums
    {
        public enum StorageType
        { 
            File,
            MongoDB
        }

        public enum SitemapChangeFreq
        { 
            Always,
            Hourly,
            Daily,
            Weekly,
            Monthly,
            Yearly,
            Never
        }
    }
}
