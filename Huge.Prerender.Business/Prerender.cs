using Huge.Prerender.Core.DataService;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;

namespace Huge.Prerender.Core
{
    public class Prerender
    {
        IDataService _dataService;

        public Prerender(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void CreateStaticContent(string key, string url)
        {
            using (var driver = new PhantomJSDriver())
            {
                string output;
                driver.Navigate().GoToUrl(url);
                try// wait till ajax has finished loading or timeout occurs.
                {
                    var element = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists(By.Id("divAjaxDataLoaded")));
                }
                catch { };
                output = driver.PageSource;
                //_dataService.Save();
            }
        }

        public void CreateStaticContent(string websiteKey, string [] urls)
        {
            using (var driver = new PhantomJSDriver())
            {
                foreach (string url in urls)
                {
                    string content;
                    driver.Navigate().GoToUrl(url);
                    //try// wait till ajax has finished loading or timeout occurs.
                    //{
                    //    var element = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists(By.Id("divAjaxDataLoaded")));
                    //}
                    //catch { };
                    content = driver.PageSource;
                    _dataService.Save(websiteKey, url, content);
                }
            }
        }

        public void ProcessSite(string key, string sitemapUrl)
        {
            SitemapParser sitemapParser = new SitemapParser(sitemapUrl);
            string[] pageUrls = sitemapParser.GetPageUrls();
            CreateStaticContent(key, pageUrls);
        }
    }
}
