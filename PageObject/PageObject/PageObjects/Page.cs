using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObject.PageObjects
{
    public class Page
    {
        protected readonly IWebDriver _webDriver;

        [FindsBy(How = How.TagName, Using = "body")]
        public IWebElement Body;

        public Page(IWebDriver driver)
        {
            _webDriver = driver;
            PageFactory.InitElements(_webDriver, this);
        }

        public IWebElement GetWebElement(string xPath)
        {
            return _webDriver.FindElement(By.XPath(xPath));
        }


        //this will search for the element until a timeout is reached
        public IWebElement WaitUntilElementExists(By elementLocator, int timeout = 10)
        {
            try
            {
                return Utils.applySleep(_webDriver, timeout).Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public IWebElement WaitUntilElementVisible(By elementLocator, int timeout = 10)
        {
            try
            {
                return Utils.applySleep(_webDriver, timeout).Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        public IWebElement WaitUntilElementClickable(By elementLocator, int timeout = 10)
        {
            try
            {
                return Utils.applySleep(_webDriver, timeout).Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }
    }
}
