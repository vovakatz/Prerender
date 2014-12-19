using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Huge.Prerender.Business
{
    public class Renderer
    {
        public void CreateStaticContent(string url)
        {
            using (var driver = new PhantomJSDriver())
            {
                string output;
                driver.Navigate().GoToUrl(url);
                    try// wait till ajax has finished loading or timeout occurs.
                    {
                        var element = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists(By.Id("divAjaxDataLoaded")));
                        //var element = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists(By.Id("toggle-list")));
                    }
                    catch { };
                    output = driver.PageSource;

                    //remove all javascript from the Find Vehicle page
                    Regex regExp = new Regex(@"<script [^>]*>[\s\S]*?</script>");
                    output = regExp.Replace(driver.PageSource, "");
            }
        }
    }
}
