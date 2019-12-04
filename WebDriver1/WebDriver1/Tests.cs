

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Edge;
using System;
using OpenQA.Selenium.Support.UI;

namespace WebDriver1
{   
    class Tests
    {
        private EdgeDriver driver;
        private string homepage =  "https://transportnsw.info/regional";

        [SetUp]
        public void SetupDriver()
        {
            driver = new EdgeDriver() { Url = homepage };
        }

        /**
            Тест 1: Ввод одинакового города для полей "from" и "to"

            Шаги: 
	            1. открыть главную страницу 
	            2. в поле "from" указать город Sydney (Central)
	            3. в поле "to" указать город Sydney (Central)
	            4. в поле "departure date" указать текущую дату
	            5. нажать кнопку "Search"

            Ожидаемый результат: под полями указазания городов появляются красные надписи "Please select from list". 
         */
        [Test]
        public void CheckEqualFromToFields()
        {
            driver.FindElementById("tniFromStop").SendKeys("Sydney");
            WaitUntilElementExists(By.Id("autosuggest-item-0"), 3).Click();

            try
            {
                driver.FindElementById("tniToStop").SendKeys("Sydney");
                WaitUntilElementExists(By.Id("autosuggest-item-0"), 3).Click();
            } catch
            {

            }

            driver.FindElementByClassName("btnpicker btnpickerenabled").SendKeys(Keys.Enter);
            driver.FindElementByClassName("daycell currmonth tablesingleday").SendKeys(Keys.Enter);
            driver.FindElementByXPath("//*[@id='tniPassengerCount']/div[1]/div/div[2]/button[2]").SendKeys(Keys.Enter);
            driver.FindElementByXPath("//*[@id='booking-content']/div[2]/tni-booking/div/div/form/div[2]/button").SendKeys(Keys.Enter);

            try
            {
                var errorMessageLocator = By.Id("error-message-box");

                Assert.IsTrue(driver.FindElements(errorMessageLocator)[1].Displayed, "Message is not displayed");
            }
            catch
            {
                Assert.Fail("Element not found.");
            }
        }

        /**
            Тест 6: Заказ билета при пустых данных в форме брони
            Шаги: 
	            1. выбрать маршрут "Sydney - Sandgate"
	            2. в расписании выбрать любое время и дату
	            3. нажать на кнопку "Search"
	            3. Выбрать любой маршрут из предложенных
	            4. Нажать кнопку "Select this trip for __$"
	            5. в появившемся окне с формами не заплняем поля

            Ожидаемый результат: все пустые поля должны быть подсвечены красным и надпись типа "These fields are required"
         */
        [Test]
        public void CheckForIncorrectDataInBookingForm()
        {
            driver.FindElementById("tniFromStop").SendKeys("Sydney");
            WaitUntilElementExists(By.Id("autosuggest-item-0"), 3).Click();

            try
            {
                driver.FindElementById("tniToStop").SendKeys("Sandgate");
                WaitUntilElementExists(By.Id("autosuggest-item-0"), 3).Click();
            }
            catch
            {
                
            }

            driver.FindElementByClassName("btnpicker btnpickerenabled").SendKeys(Keys.Enter);
            driver.FindElementsByClassName("daycell currmonth tablesingleday")[1].SendKeys(Keys.Enter);
            driver.FindElementByXPath("//*[@id='tniPassengerCount']/div[1]/div/div[2]/button[2]").SendKeys(Keys.Enter);
            driver.FindElementByXPath("//*[@id='booking-content']/div[2]/tni-booking/div/div/form/div[2]/button").SendKeys(Keys.Enter);


            var tripTicketLocator = By.ClassName("accordian-item__button");
            var ticket = WaitUntilElementExists(tripTicketLocator, 30);

            ticket.Click();

            var buttonlocator = By.ClassName("btn btn-primary btn-lg itinerary-fares__select ng-star-inserted");
            WaitUntilElementExists(buttonlocator);
            WaitUntilElementVisible(buttonlocator).Click();

            var requiredCheckBox = By.Id("i2");
            WaitUntilElementExists(requiredCheckBox).Click();

            var submitButton = By.Id("detailsConfirmationButton");

            WaitUntilElementClickable(submitButton).Click();

            try
            {
                Assert.IsTrue(driver
                    .FindElementById("contactEmailFormGroup")
                    .FindElement(By.ClassName("form-text invalidMessage ng-star-inserted")).Displayed);
            }
            catch
            {
            }
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }



        //this will search for the element until a timeout is reached
        public IWebElement WaitUntilElementExists(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public IWebElement WaitUntilElementVisible(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        public IWebElement WaitUntilElementClickable(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }
    }
}
