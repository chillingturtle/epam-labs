using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTestProject.Utilities
{
    public static class Utils
    {
        public static WebDriverWait applySleep(IWebDriver driver, int seconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }
    }
}
