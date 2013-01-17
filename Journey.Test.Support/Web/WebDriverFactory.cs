using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Journey.Test.Support.Web
{
    public class WebDriverFactory 
    {
        public static IWebDriver GetWebdriver()
        {
            string browserType = ConfigurationManager.AppSettings["BrowserType"];
            try
            {
                if (browserType.Trim().ToUpper().Equals("IE"))
                    return new InternetExplorerDriver();
                if (browserType.Trim().ToUpper().Equals("FF"))
                    return new FirefoxDriver();
                if (browserType.Trim().ToUpper().Equals("GC"))
                    return new ChromeDriver();
            }
            catch (Exception)
            {
                return new FirefoxDriver();    
            }
           return new FirefoxDriver();
        }
    }
}