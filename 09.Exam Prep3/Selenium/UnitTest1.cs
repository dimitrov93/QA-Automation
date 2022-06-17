using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Selenium
{
    public class Tests
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private const string url = "https://www.astrosofa.com/horoscope/ascendant";

        [SetUp]
        public void OpenBrowser()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Cookies.AllCookies
        }

        [TearDown]
        public void CloseBrowser()
        {
/*            this.driver.Quit();
*/        }

        [Test]
        public void ZodiacTest()
        {

            driver.Navigate().GoToUrl(url);
            Thread.Sleep(10000);
            var textEl = wait.Until(x =>
            {
                return x.FindElement(By.PartialLinkText("Accept all")).Text;
            });
            driver.FindElement(By.Id("name_id")).SendKeys("Steve");
            driver.FindElement(By.Id("month-id")).SendKeys("May");
            driver.FindElement(By.Id("day-id")).SendKeys("17");
            driver.FindElement(By.Id("year-id")).SendKeys("1987");
            driver.FindElement(By.CssSelector("#hour-id > option:nth-child(8)")).Click();
            driver.FindElement(By.CssSelector("#minute-id > option:nth-child(46)")).Click();
            driver.FindElement(By.CssSelector("#country_id-1 > option:nth-child(36)")).Click();
            driver.FindElement(By.Id("place-1")).SendKeys("Sofia");
            driver.FindElement(By.Id("SendButtonID")).Click();
        }
    }
}