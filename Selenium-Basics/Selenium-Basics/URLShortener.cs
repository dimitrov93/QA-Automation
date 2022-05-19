using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_Basics
{
    public class URLShortener
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test_Search_Wikipedia()
        {
            driver.Url = "https://shorturl.nakov.repl.co";
            var title = driver.Title;
            Assert.AreEqual(title, "URL Shortener");
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}