using Huge.Prerender.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Huge.Prerender.Core
{
    public class SitemapParser
    {
        private string _sitemapUrl;

        public SitemapParser(string sitemapUrl)
        {
            _sitemapUrl = sitemapUrl;
        }

        public List<SitemapUrl> GetPageUrls()
        {
            List<SitemapUrl> result = new List<SitemapUrl>();

            XElement sitemap = XElement.Load(_sitemapUrl);

            XName url = XName.Get("url", "http://www.sitemaps.org/schemas/sitemap/0.9");
            XName loc = XName.Get("loc", "http://www.sitemaps.org/schemas/sitemap/0.9");
            XName wait = XName.Get("wait", "http://www.mysite.com/data/blog/1.0");
            XName waitfor = XName.Get("waitfor", "http://www.mysite.com/data/blog/1.0");

            foreach (var urlElement in sitemap.Elements(url))
            {
                var locElement = urlElement.Element(loc);
                var waitElement = urlElement.Element(wait);
                var waitForElement = urlElement.Element(waitfor);

                SitemapUrl sitemapUrl = new SitemapUrl();
                if (locElement != null)
                    sitemapUrl.Loc = locElement.Value;
                if (waitElement != null)
                    sitemapUrl.WaitTime = Convert.ToInt32(waitElement.Value);
                if (waitForElement != null)
                    sitemapUrl.WaitForElementId = waitForElement.Value;

                result.Add(sitemapUrl);
            }

            return result;
        }
    }
}
