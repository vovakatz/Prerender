using Huge.Prerender.Core.DataService;
using Huge.Prerender.Core.Utilities;
using Huge.Prerender.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
            try
            {
                //var grabby = new Grabby();

                //string output = grabby.Grab();
                //Console.WriteLine(output);
                //File.WriteAllText("c:\\test.html", output);

                using (var driver = new PhantomJSDriver())
                {
                    driver.Manage().Window.Position = new Point(0, 0);
                    driver.Manage().Window.Size = new Size(1200, 768);
                    foreach (SitemapUrl url in urls)
                    {
                        if (Stop) break;
                        string content;
                        driver.Navigate().GoToUrl(url.Loc);
                        if (url.WaitForElementId != null)
                        {
                            try// wait till ajax has finished loading or timeout occurs.
                            {
                                var element = new WebDriverWait(driver, TimeSpan.FromSeconds(url.WaitTime)).Until(ExpectedConditions.ElementExists(By.Id("yoyo")));
                            }
                            catch { }
                        }
                        content = driver.PageSource;
                        _dataService.Save(websiteKey, url.Loc, content);
                        Common common = new Common();
                        driver.GetScreenshot().SaveAsFile("c:\\Temp\\" + websiteKey + "\\" + common.GetFileName(url.Loc) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        public void ProcessSite(string websiteKey, string sitemapUrl)
        {
            SitemapParser sitemapParser = new SitemapParser(sitemapUrl);
            List<SitemapUrl> pageUrls = sitemapParser.GetPageUrls();
            CreateStaticContent(websiteKey, pageUrls);
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

    public class Grabby
    {
        public string Grab()
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "C:\\Playground\\Huge.Prerender\\packages\\PhantomJS.1.9.8\\tools\\phantomjs\\phantomjs.exe",
                Arguments = string.Format("\"C:\\index.js\" http://www.townandcountrytoyota.com/Find-Vehicle#?SortBy=HighestPrice&InventoryType=New&Makes=Toyota&StartPage=1")
            };

            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        public void TestGrabby()
        {
            var grabby = new Grabby();

            string output = grabby.Grab();
            Console.WriteLine(output);
            File.WriteAllText("c:\\test.html", output);
        }
    }
}
