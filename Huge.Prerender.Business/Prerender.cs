using Huge.Prerender.Core.DataService;
using Huge.Prerender.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Huge.Prerender.Core
{
    public class Prerender
    {
        IDataService _dataService;

        public static bool Stop { get; set; }

        public Prerender(IDataService dataService)
        {
            _dataService = dataService;
            Stop = false;
        }

        public void CreateStaticContent(string websiteKey, List<SitemapUrl> urls)
        {
            using (var driver = new PhantomJSDriver())
            {
                foreach (SitemapUrl url in urls)
                {
                    if (Stop) break;
                    string content;
                    driver.Navigate().GoToUrl(url.Loc);
                    if (url.WaitForElementId != null)
                    try// wait till ajax has finished loading or timeout occurs.
                    {
                        var element = new WebDriverWait(driver, TimeSpan.FromSeconds(url.WaitTime)).Until(ExpectedConditions.ElementExists(By.Id(url.WaitForElementId)));
                    }
                    catch { };
                    content = driver.PageSource;
                    _dataService.Save(websiteKey, url.Loc, content);
                }
            }
        }

        public void ProcessSite(string websiteKey, string sitemapUrl)
        {
            SitemapParser sitemapParser = new SitemapParser(sitemapUrl);
            List<SitemapUrl> pageUrls = sitemapParser.GetPageUrls();
            CreateStaticContent(websiteKey, pageUrls);
        }
    }
}
