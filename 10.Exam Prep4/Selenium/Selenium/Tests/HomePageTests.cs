using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.PageObjects;

namespace Selenium.Tests
{
    public class HomePageTests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver(); 
        }

        [TearDown]
        public void CloseWindow()
        {
            driver.Close();
        }

        [Test]
        public void Test_HomePage_Links()
        {
            var home_page = new HomePage(driver); ;
            home_page.OpenThePage();
            Assert.That(home_page.GetPageUrl() == driver.Url);
            Assert.That(home_page.GetPageTitle, Is.EqualTo("MVC Example"));
            Assert.That(home_page.GetPageHeading, Is.EqualTo("Students Registry"));
        }
    }
}