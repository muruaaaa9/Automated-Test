using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Linq;
using Journey.Test.Support.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Journey.Test.Support
{
    public class Browser
    {
        public readonly IWebDriver _driver;
        private readonly string _baseUrl;
        private readonly IDictionary<string, Type> _pages;

        private Browser(IWebDriver driver, string baseUrl, Dictionary<string, Type> pages)
        {
            _driver = driver;
            _pages = pages;
            _baseUrl = baseUrl;
        }

        public static void Initialise(IWebDriver driver)
        {
           // var serverUrl = GetServerUrl();
            var serverUrl = GetServerUrlFromEnvironment();
            var baseUrl = serverUrl +  "/Common/Pages/Initialise.aspx?AFFCLIE=CM02&prdcls=PC&rqstyp=newmotorquote";
            var pages = new Dictionary<string, Type>
                         {
                             {serverUrl + "/Motor/AboutYourVehicle.aspx?ton_t=CTMMO&AFFCLIE=CM02&prdcls=PC&rqstyp=newmotorquote", typeof(AboutYourVehiclePage) },
                             {serverUrl + "/Motor/AboutYou.aspx", typeof(AboutYouPage) },
                             {serverUrl + "/Motor/AboutYourPolicy.aspx", typeof(AboutYourPolicyPage) },
                             {serverUrl + "/Motor/PricePage.aspx", typeof(PricePage) },
                         };
            Current = new Browser(driver, baseUrl, pages);
            Current._driver.Manage().Window.Maximize();
            Current._driver.Navigate().GoToUrl(baseUrl);
        }

        private static string GetServerUrl()
        {
            var localIpAddress = new NetworkUtil().GetLocalIpAddress();
            var serverUrl = string.IsNullOrEmpty(localIpAddress) ? "http://localhost:14510" : String.Format("http://{0}/PrivateCar", localIpAddress);
            return serverUrl;
        }

        private static string GetServerUrlFromEnvironment()
        {
            string environment = ConfigurationManager.AppSettings["Environment"];
            string url = "http://localhost:14510";
            if (environment.Trim().ToUpper().Equals("LOCAL"))
            {
                url = ConfigurationManager.AppSettings["LOCALUrl"];
            }
            else if (environment.Trim().ToUpper().Equals("QA"))
            {
                url = ConfigurationManager.AppSettings["QAUrl"];
            }
            else if (environment.Trim().ToUpper().Equals("UAT"))
            {
                url = ConfigurationManager.AppSettings["UATUrl"];
            }
            else if (environment.Trim().ToUpper().Equals("REG"))
            {
                url = ConfigurationManager.AppSettings["REGUrl"];
            }

            url = Convert.ToBoolean(ConfigurationManager.AppSettings["QSTestEnabled"])
                     ? url + @"QuestionSet"
                     : url + @"PrivateCar";

            return url;
        }

        

        public static Browser Current { get; private set; }


        public TSpecificPage NavigateTo<TSpecificPage>() where TSpecificPage : Page<TSpecificPage>
        {
            return Page<TSpecificPage>.Open(_driver, GetPageUrl<TSpecificPage>());
        }

        public static void Close()
        {
            Current._driver.Quit();
        }

        public void CaptureScreenShot(string imageName)
        {
            var screenshot = ((ITakesScreenshot) _driver).GetScreenshot();
            screenshot.SaveAsFile(imageName, ImageFormat.Jpeg);
        }

        public void Reset()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().GoToUrl(_baseUrl);
        }

        public T GetPage<T>() where T : Page<T>
        {
            var pageUrl = GetPageUrl<T>();
            const int waitTimeInSeconds = 30;
            var webDriverWait = new WebDriverWait(_driver, new TimeSpan(0, 0, waitTimeInSeconds));
            webDriverWait.Message =
                String.Format("Waited for {0} seconds - expected url to be {1}, but was {2}", waitTimeInSeconds, pageUrl, _driver.Url);
            webDriverWait.Until(driver => UrisAreEqual(driver.Url, pageUrl));
            var pageClass = _pages.First(pair => UrisAreEqual(pair.Key, _driver.Url)).Value;
            return (T)Activator.CreateInstance(pageClass, _driver);
            
        }

        private string GetPageUrl<T>() where T : Page<T>
        {
            var urlAndPageTuple = _pages.First(x => x.Value == typeof (T));
            var pageUrl = urlAndPageTuple.Key;
            return pageUrl;
        }

        private static bool UrisAreEqual(string url, string expectedUrl)
        {
            var pageUri = new Uri(url);
            var expectedUri = new Uri(expectedUrl);

            var pageAbsoluteUri = new Uri(pageUri.GetLeftPart(UriPartial.Path));   // Added to get rid of the query strings
            var pageexpectedUri = new Uri(expectedUri.GetLeftPart(UriPartial.Path));

            return pageAbsoluteUri.AbsolutePath.Contains(pageexpectedUri.AbsolutePath) && pageUri.Fragment.Contains(expectedUri.Fragment);
        }
    }

    
}
