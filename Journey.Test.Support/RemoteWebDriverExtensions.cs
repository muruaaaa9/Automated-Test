using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Journey.Test.Support
{
    public static class RemoteWebDriverExtensions
    {
        public static void WaitForAjax(this RemoteWebDriver driver)
        {
            WaitUnitJavascriptTrue(driver, "return $.active === 0");
        }

        private static void WaitUnitJavascriptTrue(RemoteWebDriver driver, string javascript)
        {
            Func<IWebDriver, bool> condition = delegate
                                                   {
                                                       var scriptResult = driver.ExecuteScript(javascript);
                                                       var isScriptActive = (bool)scriptResult;
                                                       return isScriptActive;
                                                   };
            driver.Wait().Until(condition);
        }
    }
}