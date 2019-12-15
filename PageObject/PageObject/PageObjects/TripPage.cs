using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObject.PageObjects
{
    public class TripPage : Page
    {
        [FindsBy(How = How.Id, Using = "i2")]
        public IWebElement checkbox;

        [FindsBy(How = How.Id, Using = "detailsConfirmationButton")]
        public IWebElement submitButton;

        public TripPage(IWebDriver driver) : base(driver)
        { }

        public bool CheckForError()
        {
            return _webDriver
                .FindElement(By.Id("contactEmailFormGroup"))
                .FindElement(By.ClassName("form-text invalidMessage ng-star-inserted")).Displayed;
        }
    }
}
