using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace UnitTestProject.Page_Models
{
    public class RegistrationPage : AbstractPage<RegistrationPage>
    {
        
        [FindsBy(How = How.XPath, Using = "//*[@id='submit']")]
        public IWebElement submitButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='email']")]
        public IWebElement email;

        public By errorMessagePath = By.XPath("//*[@id='jsWwarningMessages']");

        public RegistrationPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public void SkipRegistration()
        {
            this.submitButton.Click();
        }

        public bool CheckForError()
        {
            var errorMessageLocator = this.errorMessagePath;
            return Driver.FindElements(errorMessageLocator)[1].Displayed;
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
