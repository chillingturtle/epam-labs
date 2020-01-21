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
        public By checkBoxError = By.XPath("//*[@id='termsAndConditionsFormGroup']/div[1]/div/small");

        public TripPage(IWebDriver driver) : base(driver)
        { }

        public bool CheckForError()
        {
            var formError = Driver
                .FindElement(By.Id("contactEmailFormGroup"))
                .FindElement(By.ClassName("form-text invalidMessage ng-star-inserted")).Displayed;
            var checkBoxErrorM = WaitUntilElementExists(checkBoxError).Displayed;

            return formError || checkBoxErrorM;
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
