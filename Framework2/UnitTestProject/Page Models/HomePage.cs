using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using UnitTestProject.Utilities;

namespace UnitTestProject.Page_Models
{
    public class HomePage : AbstractPage<HomePage>
    {
        private const string PageUrl = "https://transportnsw.info/regional";

        [FindsBy(How = How.Id, Using = "tniFromStop")]
        public IWebElement whereFromInput;

        [FindsBy(How = How.Id, Using = "tniToStop")]
        public IWebElement whereToInput;

        [FindsBy(How = How.ClassName, Using = "btnpicker")]
        public IWebElement datePickerButton;

        public By dayCell = By.ClassName("daycell currmonth tablesingleday");

        [FindsBy(How = How.XPath, Using = "//*[@id='tniPassengerCount']/div[1]/div/div[2]/button[2]")]
        public IWebElement passengersIncreaseButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='booking-content']/div[2]/tni-booking/div/div/form/div[2]/button")]
        public IWebElement submitFormButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='block-trainlinklogo']/div/p/a[2]")]
        public IWebElement registerLink;

        [FindsBy(How = How.ClassName, Using = "daycell disabled")]
        public IWebElement invalidCell;
        

        public HomePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public void FillTicketsBookingForm(string from, string to, bool correct = true, int passCount = 1, int daysNext = 1)
        {
            InsertValueInWhereFrom(from);
            InsertValueInWhereTo(to);
            SelectTommorowDate(correct, daysNext);

            while (passCount > 0)
            {
                passengersIncreaseButton.Click();
                passCount--;
            }

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

        public void SelectTommorowDate(bool valid, int daysNext = 1)
        {
            datePickerButton.Click();

            var cell = Driver.FindElements(dayCell)[daysNext];

            if (valid)
            {
                cell.Click();
            }
            else
            {
                invalidCell.Click();
            }
        }

        public bool CheckForError()
        {
            var errorMessageLocator = By.Id("error-message-box");
            var fl = false;

            Utils.applySleep(Driver, 3);

            foreach (var el in Driver.FindElements(errorMessageLocator))
            {
                fl = fl || el.Displayed;
            }

            return fl;
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
