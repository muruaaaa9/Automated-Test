using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Journey.Test.Support.Pages
{
    public class PricePage : Page<PricePage>
    {
        private RemoteWebDriver _driver;

        public PricePage(RemoteWebDriver driver)
            : base(driver)
        {
            _driver = driver;
        }

        private IWebElement WaitUntilById(string idToFind)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            var webElement = wait.Until(d =>
            {
                var element = Driver.FindElement(By.Id(idToFind));
                if (element.Displayed)
                    return element;
                return null;
            });
            return webElement;
        }


        public PricePage AssertPricePage()
        {
            //System.Threading.Thread.Sleep(5000);
            WaitUntilById("logo");
            Assert.That(AssertTitle(), Is.EqualTo(true));
            return this;
        }

        private bool AssertTitle()
        {
            var yourQuotes = "PRICEPAGE";
            return Driver.Title.Trim().ToUpper().Contains(yourQuotes);
        }
    }
}