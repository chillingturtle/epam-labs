using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("transportnsw.info");

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

            driver.FindElementById("tniFromTripLocation").SendKeys("Sydney");
            driver.FindElementById("tniToTripLocation").SendKeys("Sydney");
            driver.FindElementByClassName("btn btn-primary go-btn").SendKeys(Keys.Enter);
        }
    }
}
