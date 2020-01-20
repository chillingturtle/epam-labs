using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UnitTestProject.Page_Models
{
    public class TripPage : AbstractPage<TripPage>
    {
        //[FindsBy(How = How.Id, Using = "i2")]
        public IWebElement checkbox;

        public By checkBox = By.Id("i2");
        public By submitButton = By.Id("detailsConfirmationButton");

        public TripPage(IWebDriver driver) : base(driver)
        { }

        public bool CheckForError()
        {
            return Driver
                .FindElement(By.Id("contactEmailFormGroup"))
                .FindElement(By.ClassName("form-text invalidMessage ng-star-inserted")).Displayed;
        }

        public IWebElement getCheckBox()
        {
            return WaitUntilElementExists(checkBox, 30);
        }

        public IWebElement getSubmitButton()
        {
            return WaitUntilElementClickable(submitButton);
        }

        public override TripPage OpenPage()
        { return this; }
    }
}
