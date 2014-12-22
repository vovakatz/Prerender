using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;

namespace Huge.Prerender.Business
{
    public class Renderer
    {
        IDataPersister _persister;

        public Renderer(IDataPersister persister)
        {
            _persister = persister;
        }

        public void CreateStaticContent(string url)
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
                _persister.Save();
            }
        }

        public void CreateStaticContent(string [] urls)
        {
            using (var driver = new PhantomJSDriver())
            {
                foreach (string url in urls)
                {
                    string output;
                    driver.Navigate().GoToUrl(url);
                    try// wait till ajax has finished loading or timeout occurs.
                    {
                        var element = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists(By.Id("divAjaxDataLoaded")));
                    }
                    catch { };
                    output = driver.PageSource;
                    _persister.Save();
                }
            }
        }
    }
}
