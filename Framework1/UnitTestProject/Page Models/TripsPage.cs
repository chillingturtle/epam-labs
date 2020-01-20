using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UnitTestProject.Page_Models
{
    public class TripsPage : AbstractPage<TripsPage>
    {
        //[FindsBy(How = How.ClassName, Using = )]
        public IWebElement ticket;

        public TripsPage(IWebDriver driver) : base(driver)
        { ticket = WaitUntilElementExists(By.ClassName("accordian-item__button"), 20); }

        public IWebElement GetTicketButton()
        {
            return WaitUntilElementVisible(By.ClassName("btn btn-primary btn-lg itinerary-fares__select ng-star-inserted"));
        }

        public override TripsPage OpenPage()
        { return this; }
    }
}
