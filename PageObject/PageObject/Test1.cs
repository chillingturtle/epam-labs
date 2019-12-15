using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using PageObject.PageObjects;

namespace PageObject
{
    public class Test1
    {
        private EdgeDriver driver;
        private string homepage = "https://transportnsw.info/regional";

        [SetUp]
        public void SetupDriver()
        {
            driver = new EdgeDriver() { Url = homepage };
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
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
            var homePage = new HomePage(driver);

            try
            {
                homePage.FillTicketsBookingForm("Sydney", "Sydney");

                Assert.IsTrue(homePage.CheckForError(), "Message is not displayed");
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
            var homePage = new HomePage(driver);
            homePage.FillTicketsBookingForm("Sydney", "Sandgate");

            var tripsPage = new TripsPage(driver);

            tripsPage.ticket.Click();
            tripsPage.GetTicketButton().Click();

            var tripPage = new TripPage(driver);

            tripPage.checkbox.Click();
            tripPage.submitButton.Click();

            try
            {
                Assert.IsTrue(tripPage.CheckForError());
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
    }
}
