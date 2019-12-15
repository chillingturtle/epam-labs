using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace PageObject.PageObjects
{
    public class TripsPage : Page
    {
        [FindsBy(How = How.ClassName, Using = "accordian-item__button")]
        public IWebElement ticket;

        public TripsPage(IWebDriver driver) : base(driver)
        { }

        public IWebElement GetTicketButton()
        {
            return WaitUntilElementVisible(By.ClassName("btn btn-primary btn-lg itinerary-fares__select ng-star-inserted"));
        }
    }
}
