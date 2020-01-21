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

                    Assert.IsTrue(regPage.CheckForError(), "Message is not displayed");
                }
                catch (Exception e)
                {
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
        }

        /**
            Тест 3: Проверка поиска маршрута без ввода пункта назначения/отправления

            Шаги: 
	            1. на главной странице попытаться изменить поля "from" и "to"
	            2. оставить их пустыми;
	            3. попытаться начать поиск маршрута, нажав на кнопку "Search". 

            Ожидаемый результат: кнопка поиска расписания маршрутов неактивна,
            вывод сообщений "Fill the station name".
        */

        [Test]
        public void CheckEmptyFromToForm()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();

                try
                {
                    homePage.submitFormButton.Click();

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
            Тест 5: Заказ билета при вводе некорректных данных в форме брони
            Шаги: 
	            1. выбрать маршрут "Sydney - Sandgate "
	            2. в расписании выбрать любое время и дату
	            3. нажать на кнопку "Search"
	            3. Выбрать любой маршрут из предложенных
	            4. Нажать кнопку "Select this trip for __$"
	            5. в появившемся окне с формами указываем некорректый телефон и email
	
            Ожидаемый результат: поля ввода телефона и email должны быть подсвечены красным и надпись типа "Please enter valid phone number/email"
        */
        [Test]
        public void CheckForIncorrectDataInBookingForm()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();
                homePage.FillTicketsBookingForm("Sydney", "Fairfield");

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
        public void CheckForNoDataInBookingForm()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();
                homePage.FillTicketsBookingForm("Sydney", "Fairfield");

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
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
        }

        /**
            Тест 7: Заказ билета при неотмеченном checkbox terms and conditions
            Шаги: 
	            1. выбрать маршрут "Sydney - Sandgate"
	            2. в расписании выбрать любое время и дату
	            3. нажать на кнопку "Search"
	            3. Выбрать любой маршрут из предложенных
	            4. Нажать кнопку "Select this trip for __$"
	            5. в появившемся окне с формами заплняем поля
	            6. не соглашаемся с terms and conditions, а нажимаем на Go to secure payment

            Ожидаемый результат: checkbox должен быть подсвечен красным
        */
        [Test]
        public void CheckForNoCheckBoxBookingForm()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();
                homePage.FillTicketsBookingForm("Sydney", "Fairfield");

                var tripsPage = new TripsPage(Driver);

                tripsPage.ticket.Click();
                tripsPage.GetTicketButton().Click();

                var tripPage = new TripPage(Driver);

                //tripPage.getCheckBox().Click();
                tripPage.getSubmitButton().Click();

                try
                {
                    Assert.IsTrue(tripPage.CheckForError());
                }
                catch (Exception e)
                {
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
        }

        /**
            Тест 8: Заказ билета на прошедшее время

            Шаги:
	            1. в форме ввода маршрута ввести маршрут "Sydney - Sandgate"
	            2. попытаться выбрать вчерашнюю дату

            Ожидаемый результат: все прошедшие дни должны быть неактивны
        */
        [Test]
        public void CheckForPastDatePick()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();

                try
                {
                    homePage.FillTicketsBookingForm("Sydney", "Fairfield", false);
                    Assert.IsTrue(homePage.CheckForError());
                }
                catch (Exception e)
                {
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
        }

        /**
            Тест 9: Проверка максимального количества пассажиров на заказ

            Шаги: 
	            1. при выборе маршрута попробовать указать 10 людей;

            Ожидаемый результат: вывод предупреждения о превышении максимально возможного количества пассажиров 
            и указание контакта для связи, чтобы организовать групповую поездку.
        */
        [Test]
        public void CheckForMaxPassCount()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();

                try
                {
                    homePage.FillTicketsBookingForm("Sydney", "Fairfield", true, 10);
                    Assert.IsTrue(homePage.CheckForError());
                }
                catch (Exception e)
                {
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
        }

        /**
            Тест 10: Проверка максимальной даты заказа

            Шаги: 
	            1. при выборе маршрута попробовать указать дату оправления через год;

            Ожидаемый результат: запрет выбора дня и месяца, которые наступят больше, чем через 90 дней с момента заказа.
        */
        [Test]
        public void CheckForMaxDatePick()
        {
            TakeScreenshotOnTestFailure(() =>
            {
                var homePage = new HomePage(Driver).OpenPage();

                try
                {
                    homePage.FillTicketsBookingForm("Sydney", "Fairfield", true, 1, 90);
                    Assert.IsTrue(homePage.CheckForError());
                }
                catch (Exception e)
                {
                    Assert.Fail("Element not found.");
                    throw e;
                }
            });
        }
    }
}