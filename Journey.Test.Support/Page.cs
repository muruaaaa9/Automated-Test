using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Journey.Test.Support
{
    public interface IPage
    {
    }


    public class Page<TSpecificPage> : IPage where TSpecificPage : Page<TSpecificPage>
    {
        protected readonly RemoteWebDriver Driver;
        protected Page(RemoteWebDriver driver)
        {
            Driver = driver;
        }

        public string Title
        {
            get { return Driver.Title; }
        }

        public Cookie GetCookieNamed(string cookieName)
        {
            return Driver.Manage().Cookies.GetCookieNamed(cookieName);
        }

        public static TSpecificPage Open(IWebDriver driver, string baseUrl)
        {
            var page = ((TSpecificPage) Activator.CreateInstance(typeof (TSpecificPage), driver));
            page.Driver.Navigate().GoToUrl(baseUrl);
            return page;
        }

        public static TSpecificPage Open()
        {
            var page = Browser.Current.NavigateTo<TSpecificPage>();
            page.Driver.WaitForAjax();
            return page;
        }

        public void WaitForAjax()
        {
            Driver.WaitForAjax();
        }

        protected IWebElement GetCustomSelectText(string idToFind)
        {
            IWebElement selectElement = null;
            var ulClassName = "ui-selectmenu-status";
            var parentElement = Driver.FindElement(By.Id(idToFind));
            selectElement = parentElement.FindElement(By.ClassName(ulClassName.Trim()));
            return selectElement;
        }

        protected IWebElement GetCustomRadioButtonElement(string idToFind, string value)
        {
            IWebElement radioElement = null;
            var parentElement = Driver.FindElement(By.Id(idToFind));
            var labelTags = parentElement.FindElements(By.TagName("label"));
            foreach (var labelTag in labelTags)
            {
                var radioButtonId = labelTag.GetAttribute("for");
                radioElement = Driver.FindElementById(radioButtonId);
                if (labelTag.Text.Trim().ToUpper().Equals(value.ToUpper())) break;
            }
            return radioElement;
        }

        protected void SetSliderValue(string idToFind, string value)
        {
            var parentElement = Driver.FindElementById(idToFind);
            var spanTags = parentElement.FindElements(By.TagName("span"));
            foreach (var spanTag in spanTags)
            {
                if(spanTag.Text.Trim().ToUpper().Equals(value.ToUpper()))
                {
                    spanTag.Click();
                    break;
                }
            }
        }

        protected string GetSelectedDropdownValue(string id)
        {
            var selectElement = new SelectElement(Driver.FindElementById(id));
            return selectElement.SelectedOption.Text;
        }
        
        protected void CustomRadioIconListClick(string idToFind, string text)
        {
            var parentElement = Driver.FindElement(By.Id(idToFind));                    //Id 
            var labelTags = parentElement.FindElements(By.TagName("label"));
            foreach(var labelTag in labelTags)
            {
                if (labelTag.Text.Trim().ToUpper().Equals(text.ToUpper().Trim()))
                { 
                    labelTag.Click();
                    break;
                }
            }
        }

        protected void CustomRadioIconListClick(string idToFind, string className, string text)
        {
            var parentElement = Driver.FindElement(By.ClassName(className));                    //Id 
            var labelTags = parentElement.FindElements(By.TagName("label"));
            foreach(var labelTag in labelTags)
            {
                if (labelTag.Text.Trim().ToUpper().Equals(text.ToUpper().Trim()))
                { 
                    labelTag.Click();
                    break;
                }
            }
        }


        protected void CustomRadioIconListByClassClick(string idToFind, string text)
        {
            var parentElement = Driver.FindElement(By.ClassName(idToFind));            //Class names can be used if two questions has the same id's
            var labelTags = parentElement.FindElements(By.TagName("label"));
            foreach (var labelTag in labelTags)
            {
                if (labelTag.Text.Trim().ToUpper().Equals(text.ToUpper().Trim()))
                {
                    labelTag.Click();
                    break;
                }
            }
        }

        protected void CustomRadioIconLongListClick(string idToFind, string text)
        {
            var parentElement = Driver.FindElement(By.Id(idToFind));
            var labelTags = parentElement.FindElements(By.TagName("label"));
            foreach (var labelTag in labelTags)
            {
                if (labelTag.Text.Trim().ToUpper().Contains(text.ToUpper().Trim()))
                {
                    labelTag.Click();
                    break;
                }
            }
        }

        protected void CustomSelectCalendarByText(string idToFind, string text)
        {
            var cssSelector = "#" + idToFind + " a.datePickerLaunch";
            var anchorTag = Driver.FindElementByCssSelector(cssSelector);
            anchorTag.Click();
            SelectTextFromPullDownMenu(text);
        }

        //protected void CustomSelectDropdownByText(string id, string className, string text)
        //{
        //    var anchorTag = Driver.FindElementById(id);
        //    anchorTag.Click();
        //    SelectTextFromPullDownMenu(text);
        //}

        protected void CustomSelectDropdownByText(string id, string className, string text)
        {
            var anchorTag = Driver.FindElementById(id);
            anchorTag.Click();
            var ulClassName = "ul." + className.Trim() + " li";
            var cssAnchorTags = Driver.FindElements(By.CssSelector(ulClassName.Trim()));
            foreach (var cssAnchorTag in cssAnchorTags)
            {
                var findElement = cssAnchorTag.FindElement(By.TagName("a"));
                if (findElement.Text.Trim().Equals(text))
                {
                    findElement.Click();
                    break;
                }
            }
        }

        protected  void SelectTextFromPullDownMenu(string description)
        {
            var itemFound = WaitUntilByLinkText(description.Trim());
            itemFound.Click();
        }

        protected void CustomSelectTextBoxByText(string id, string threeLetterDescription)
        {
            IWebElement inputTextBox = Driver.FindElement(By.Id(id));
            inputTextBox.Clear();
            inputTextBox.SendKeys(threeLetterDescription);
        }

        public readonly Dictionary<bool, string> YesNoMap = new Dictionary<bool, string>
                                                                            {
                                                                                     {true,"Yes"},
                                                                                     {false,"No"}
                                                                            };

        protected void FillIn(string selector, string value, bool tabOut = false)
        {
            var textBox = Driver.FindElementById(selector);
            textBox.Clear();
            textBox.SendKeys(value + Keys.Tab);
            if(tabOut)
                textBox.SendKeys(Keys.Tab);
        }
        
        public string GetBuildNumber()
        {
            return Driver.FindElementById("build-number").Text;
        }

        public List<string> GetErrors()
        {
            var errorsContainer = Driver.FindElementById("error-messages");
            var errors = errorsContainer.FindElements(By.TagName("li")).Select(element => element.Text).ToList();
            return errors;
        }

        public bool HasErrors()
        {
            return GetErrors().Count > 0;
        }

        public void CloseErrors()
        {
            var errorsContainer = Driver.FindElementById("error-messages");
            var closeLink = errorsContainer.FindElement(By.ClassName("close"));
            closeLink.Click();
        }

        protected IWebElement WaitUntil(string idToFind)
        {
            Thread.Sleep(300);
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

        protected IWebElement NextButton(string idToFind)
        {
           
            return WaitUntil(idToFind);
        }

        protected IWebElement WaitUntilByLinkText(string text)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            var webElement = wait.Until(d =>
            {
                var element = Driver.FindElement(By.LinkText(text));
                if (element.Displayed)
                    return element;
                return null;
            });
            return webElement;
        }

        protected IWebElement WaitUntilAddAnotherAppears(string className, string text)
        {
            Thread.Sleep(2000);
            IWebElement anchorAddAnother = null;
            var parentElement = Driver.FindElement(By.ClassName(className));
            var anchorTags = parentElement.FindElements(By.TagName("a"));
            foreach (var anchor in anchorTags)
            {
                if (anchor.Text.Trim().ToUpper().Equals(text.ToUpper().Trim()))
                {
                    if (anchor.Displayed)
                        anchorAddAnother = anchor;
                }
            }
            return anchorAddAnother;
        }

        //protected IWebElement WaitUntilAddPersonAppears(string id, string text)
        //{
        //    Thread.Sleep(2000);
        //    IWebElement anchorAddAnother = null;
        //    var parentElement = Driver.FindElement(By.Id(id));
        //    var anchorTags = parentElement.FindElements(By.TagName("a"));
        //    foreach (var anchor in anchorTags)
        //    {
        //        if (anchor.Text.Trim().ToUpper().Equals(text.ToUpper().Trim()))
        //        {
        //            if (anchor.Displayed)
        //                anchorAddAnother = anchor;
        //        }
        //    }
        //    return anchorAddAnother;
        //}

        protected bool IsTheQuestionDisplayed(string id)
        {
            Thread.Sleep(300);
            if (Driver.FindElement(By.Id(id)).Displayed)
            {
                return true;
            }
            return false;
        }
    }
}