using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Selenium
{
    public class SelniumTests
    {
        private WebDriver driver;
        private const string url = "http://localhost:8080/";

        [SetUp]
        public void OpeBrowser()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [TearDown]
        public void CloseBrowser()
        {
            this.driver.Quit();
        }

        [Test]
        public void Test_Open_And_Ensure_It_Holds_Table()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Short URLs")).Click();
            var theTable = driver.FindElement(By.CssSelector("body > main > table"));
            Assert.IsNotNull(theTable);
            var allContacts = driver.FindElements(By.CssSelector("body > main > table > tbody > tr"));
            Assert.AreEqual(3, allContacts.Count);
        }

        [Test]
        public void Test_Create()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Add URL")).Click();
            driver.FindElement(By.CssSelector("#url")).SendKeys("https://www.google.com/");
            driver.FindElement(By.CssSelector("#code")).SendKeys("gog");
            driver.FindElement(By.CssSelector("td > button")).Click();
        }
    }
}