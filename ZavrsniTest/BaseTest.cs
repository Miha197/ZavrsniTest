using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using TestLogger;
using System.Linq;


namespace AutomatedTests
{
    class BaseTest
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected void GoToURL(string url)
        {
            Logger.Info($"Opening URL: {url}");
            Driver.Navigate().GoToUrl(url);
        }

        protected IWebElement FindMyElement(By Selector)
        {
            IWebElement returnElement = null;
            Logger.Info($"Looking for element: <{Selector}>");

            try
            {
                returnElement = Driver.FindElement(Selector);
            }
            catch (NoSuchElementException)
            {
                Logger.Error($"Can't find element: <{Selector}>");
            }

            if (returnElement != null)
            {
                Logger.Info($"Element: <{Selector}> found.");
            }
            return returnElement;
        }

        protected IWebElement WaitForElement(Func<IWebDriver, IWebElement> ExpectedConditions)
        {
            IWebElement returnElement = null;
            Logger.Info("Waiting for element.");

            try
            {
                returnElement = Wait.Until(ExpectedConditions);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Error("Can't Wait for element.");
            }

            if (returnElement != null)
            {
                Logger.Info("Element found.");
            }
            return returnElement;
        }

        protected bool ElementExists(By Selector)
        {
            IWebElement returnElement = null;
            try
            {
                returnElement = Wait.Until(EC.ElementExists(Selector));
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Error("Can't Wait for element.");
            }
            return returnElement != null;
        }

        public void PopulateInput(By Selector, string TextToType)
        {
            Logger.Info($"Populate input element: <{Selector}> = '{TextToType}'");
            FindMyElement(Selector).SendKeys(TextToType);
        }

        protected void ExplicitWait(int waitTime)
        {
            Logger.Info($"Sleeping: {waitTime}ms");
            System.Threading.Thread.Sleep(waitTime);
        }

    }
}
