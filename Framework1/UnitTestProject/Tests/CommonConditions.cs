using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using UnitTestProject.Driver;

namespace UnitTestProject.Tests
{
    public class CommonConditions
    {
        protected IWebDriver Driver;

        [SetUp]
        public void SetDriver()
        {
            Driver = DriverSingleton.GetDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        protected void TakeScreenshotOnTestFailure(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                var screenshotsFolder = AppDomain.CurrentDomain.BaseDirectory + @"\Screenshots";
                Directory.CreateDirectory(screenshotsFolder);
                var screenshot = Driver.TakeScreenshot();
                screenshot.SaveAsFile(screenshotsFolder +
                                      @"\Screenshot" + DateTime.Now.ToString("yy-MM-dd_hh-mm-ss") + ".png",
                    ScreenshotImageFormat.Png);
                throw;
            }
        }

        [TearDown]
        public void QuitDriver()
        {
            DriverSingleton.CloseDriver();
        }
    }
}
