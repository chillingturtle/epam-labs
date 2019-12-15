using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObject
{
    public static class Utils
    {
        public static WebDriverWait applySleep(IWebDriver driver, int seconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }
    }
}
