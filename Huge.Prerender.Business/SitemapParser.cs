using System.Collections.Generic;
using System.Xml.Linq;

namespace Huge.Prerender.Business
{
    public class SitemapParser
    {
        private string _sitemapUrl;

        public SitemapParser(string sitemapUrl)
        {
            _sitemapUrl = sitemapUrl;
        }

        public List<string> GetPageUrls()
        {
            List<string> result = new List<string>();

            XElement sitemap = XElement.Load(_sitemapUrl);

            XName url = XName.Get("url", "http://www.sitemaps.org/schemas/sitemap/0.9");
            XName loc = XName.Get("loc", "http://www.sitemaps.org/schemas/sitemap/0.9");

            foreach (var urlElement in sitemap.Elements(url))
            {
                var locElement = urlElement.Element(loc);
                result.Add(locElement.Value);
            }

            return result;
        }
    }
}
