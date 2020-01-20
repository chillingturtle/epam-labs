using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace UnitTestProject.Page_Models
{
    public class HomePage : AbstractPage<HomePage>
    {
        private const string PageUrl = "https://transportnsw.info/regional";

        [FindsBy(How = How.Id, Using = "tniFromStop")]
        public IWebElement whereFromInput;

        [FindsBy(How = How.Id, Using = "tniToStop")]
        public IWebElement whereToInput;

        [FindsBy(How = How.ClassName, Using = "btnpicker btnpickerenabled")]
        public IWebElement datePickerButton;

        [FindsBy(How = How.ClassName, Using = "daycell currmonth tablesingleday")]
        public IWebElement dayCell;

        [FindsBy(How = How.XPath, Using = "//*[@id='tniPassengerCount']/div[1]/div/div[2]/button[2]")]
        public IWebElement passengersIncreaseButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='booking-content']/div[2]/tni-booking/div/div/form/div[2]/button")]
        public IWebElement submitFormButton;

        public HomePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public void FillTicketsBookingForm(string from, string to)
        {
            InsertValueInWhereFrom(from);
            InsertValueInWhereTo(to);
            SelectTommorowDate();
            passengersIncreaseButton.Click();
            submitFormButton.Click();
        }

        public void InsertValueInWhereTo(string value)
        {
            whereToInput.SendKeys(value);
            WaitUntilElementExists(By.Id("autosuggest-item-0"), 3).Click();
        }

        public void InsertValueInWhereFrom(string value)
        {
            whereFromInput.SendKeys(value);
            WaitUntilElementExists(By.Id("autosuggest-item-0"), 3).Click();
        }

        public void SelectTommorowDate()
        {
            datePickerButton.Click();
            dayCell.Click();
        }

        public bool CheckForError()
        {
            var errorMessageLocator = By.Id("error-message-box");
            return Driver.FindElements(errorMessageLocator)[1].Displayed;
        }

        public override HomePage OpenPage()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            return this;
        }

        private IWebElement GetWebElement(string xPath)
        {
            return Driver.FindElement(By.XPath(xPath));
        }
    }
}
