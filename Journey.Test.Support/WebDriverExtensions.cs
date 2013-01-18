using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Journey.Test.Support
{
    public static class WebDriverExtensions
    {
        public static bool IsVisible(this IWebDriver driver, By selector)
        {
            try
            {
                driver.FindElement(selector);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void WaitUnitPageLoads(this IWebDriver driver, By selector)
        {
            driver.Wait().Until(x => x.IsVisible(selector));
        }

        public static WebDriverWait Wait(this IWebDriver driver)
        {
            const int timeoutInSeconds = 10;
            return new WebDriverWait(driver, new TimeSpan(0, 0, timeoutInSeconds));
        }
    }
}
