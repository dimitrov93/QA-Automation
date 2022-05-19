using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;

namespace Selenium_Basics
{
    public class URLShortener
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            if (!Debugger.IsAttached)
            {
                options.AddArgument("--headless");
            }
            
            driver = new ChromeDriver(options);
        }

        [Test]
        public void Test_Open_Page()
        {
            driver.Url = "https://shorturl.nakov.repl.co";
            var title = driver.Title;
            Assert.AreEqual(title, "URL Shortener");
        }

        [Test]
        public void Test_Add_URL()
        {
            driver.Url = "https://shorturl.nakov.repl.co";
            driver.FindElement(By.CssSelector("body > header > a:nth-child(5)")).Click();
            driver.FindElement(By.CssSelector("#url")).SendKeys("https://nakov.com");
            driver.FindElement(By.CssSelector("body > main > form > table > tbody > tr:nth-child(3) > td > button")).Click();
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}