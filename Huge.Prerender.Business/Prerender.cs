using Huge.Prerender.Core.DataService;
using Huge.Prerender.Core.Utilities;
using Huge.Prerender.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

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

        public void ProcessSite(string websiteKey, string sitemapUrl)
        {
            SitemapParser sitemapParser = new SitemapParser(sitemapUrl);
            List<SitemapUrl> pageUrls = sitemapParser.GetPageUrls();
            CreateStaticContent(websiteKey, pageUrls);
        }

        public string CreateStaticContent(string websiteKey, string url)
        {
            using (var driver = new PhantomJSDriver())
            {
                SetupBrowserWindow(driver);
                string content;
                driver.Navigate().GoToUrl(url);
                content = driver.PageSource;
                _dataService.Save(websiteKey, url, content);
                Common common = new Common();
                driver.GetScreenshot().SaveAsFile("c:\\Temp\\" + websiteKey + "\\" + common.GetFileName(url) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                return content;
            }
        }

        public void CreateStaticContent(string websiteKey, List<SitemapUrl> urls)
        {
            using (var driver = new PhantomJSDriver())
            {
                SetupBrowserWindow(driver);
                foreach (SitemapUrl url in urls)
                {
                    if (Stop) break;
                    string content;
                    driver.Navigate().GoToUrl(url.Loc);
                    if (url.WaitForElementId != null)
                    {
                        try// wait till ajax has finished loading or timeout occurs.
                        {
                            var element = new WebDriverWait(driver, TimeSpan.FromSeconds(url.WaitTime)).Until(ExpectedConditions.ElementExists(By.Id(url.WaitForElementId)));
                        }
                        catch { }
                    }
                    else
                    {
                        int count = 0;
                        while (true) // Handle timeout somewhere
                        {
                            var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                            if (ajaxIsComplete)
                                break;
                            Thread.Sleep(100);
                            count++;
                        }
                    }

                    content = driver.PageSource;
                    _dataService.Save(websiteKey, url.Loc, content);
                    Common common = new Common();
                    driver.GetScreenshot().SaveAsFile("c:\\Temp\\" + websiteKey + "\\" + common.GetFileName(url.Loc) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        public string GetStaticContent(string websiteKey, string url)
        {
            string content = _dataService.GetContent(websiteKey, url);
            if (content == null)
                content = CreateStaticContent(websiteKey, url);
            return content;
        }

        private void SetupBrowserWindow(PhantomJSDriver driver)
        {
            driver.Manage().Window.Position = new Point(0, 0);
            driver.Manage().Window.Size = new Size(1200, 768);
        }
    }

    public class PrerenderTest
    {
        public void TestCreateStaticContent()
        {
            List<SitemapUrl> urls = new List<SitemapUrl>();
            SitemapUrl url = new SitemapUrl()
            {
                Loc = "http://www.townandcountrytoyota.com/Find-Vehicle#?SortBy=HighestPrice&InventoryType=New&Makes=Toyota&StartPage=1"
            };
            urls.Add(url);

            Prerender prerender = new Prerender(new FileDataService());
            prerender.CreateStaticContent("test",urls);
        }
    }
}
