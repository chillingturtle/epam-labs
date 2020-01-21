using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using UnitTestProject.Extensions;
using TestDataReader = UnitTestProject.Service.TestDataReader;
using HomePage = UnitTestProject.Page_Models.HomePage;
using UnitTestProject.Page_Models;

namespace UnitTestProject.Tests
{
    [TestFixture]
    [Category("All")]
    public class WebTests : CommonConditions
    {

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
        //[Category("PastDate")]
        public void CheckEqualFromToFields()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();

                try
                {
                    homePage.FillTicketsBookingForm("Sydney", "Sydney");

                    Assert.IsTrue(homePage.CheckForError(), "Message is not displayed");
                }
                catch (Exception e)
                {
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
        }

        /**
            Тест 2: Проверка регистрации пользователя с пустыми полями

            Шаги: 
	            1. навести курсор на кнопку Sign up
	            2. оставить все поля пустыми
	            3. нажать на кнопку "Submit";

            Ожидаемый результат: новый пользователь не должен быть создан, 
            выделяются поля, обязательные для заполнения и рядом выводятся красные сообщения "Please enter/fill ...".
        */

        [Test]
        public void CheckEmptyRegistrationForm()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();

                try
                {
                    homePage.registerLink.Click();
                    var regPage = new RegistrationPage(Driver);
                    regPage.SkipRegistration();

                    Assert.IsTrue(homePage.CheckForError(), "Message is not displayed");
                }
                catch (Exception e)
                {
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
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
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();
                homePage.FillTicketsBookingForm("Sydney", "Sandgate");

                var tripsPage = new TripsPage(Driver);

                tripsPage.ticket.Click();
                tripsPage.GetTicketButton().Click();

                var tripPage = new TripPage(Driver);

                tripPage.getCheckBox().Click();
                tripPage.getSubmitButton().Click();

                try
                {
                    Assert.IsTrue(tripPage.CheckForError());
                }
                catch (Exception e)
                {
                    throw e;
                }
            });
        }
    }
}