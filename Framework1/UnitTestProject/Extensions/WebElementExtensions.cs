using OpenQA.Selenium;

namespace UnitTestProject.Extensions
{
    public static class WebElementExtensions
    {
        public static bool IsEnabled(this IWebElement webElement, string @class)
        {
            return webElement.GetAttribute("class").Contains(@class);
        }
    }
}