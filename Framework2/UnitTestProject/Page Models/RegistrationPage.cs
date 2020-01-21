using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace UnitTestProject.Page_Models
{
    public class RegistrationPage : AbstractPage<RegistrationPage>
    {
        
        public By submitButton = By.XPath("//*[@id='submit']");
        public By email = By.XPath("//*[@id='email']");
        public By errorMessagePath = By.XPath("//*[@id='jsWwarningMessages']");

        public RegistrationPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public void SkipRegistration()
        {
            WaitUntilElementExists(this.submitButton).Click();
        }

        public bool CheckForError()
        {
            return WaitUntilElementExists(this.errorMessagePath).Displayed;
        }

        public override RegistrationPage OpenPage()
        {
            return this;
        }

        private IWebElement GetWebElement(string xPath)
        {
            return Driver.FindElement(By.XPath(xPath));
        }
    }
}
