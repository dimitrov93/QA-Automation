using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.PageObjects;
using System;

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


        [Test]
        public void Test_HomePage_Link_Works()
        {
            var home_page = new HomePage(driver); ;
            home_page.OpenThePage();
            home_page.HomeLink.Click();
            Assert.That(home_page.GetPageUrl() == driver.Url);
            Assert.That(home_page.GetPageTitle, Is.EqualTo("MVC Example"));
            Assert.That(home_page.GetPageHeading, Is.EqualTo("Students Registry"));
        }

        [Test]
        public void Test_ViewStudents_Link_Works()
        {
            var home_page = new HomePage(driver); ;
            home_page.OpenThePage();
            home_page.ViewStudentsLink.Click();
            var viewStudents = new ViewStudents(driver);
            Assert.That(viewStudents.GetPageTitle, Is.EqualTo("Students"));
            Assert.That(viewStudents.GetRegisteredStudents().Length, Is.EqualTo(3));
        }

        [Test]
        public void Test_AddStudents_Link_Works()
        {
            var home_page = new HomePage(driver);
            home_page.OpenThePage();
            home_page.AddStudentsLink.Click();
            var add_students = new AddStudents(driver);
            Assert.That(add_students.GetPageTitle(), Is.EqualTo("Add Student"));
            Assert.That(driver.Url, Is.EqualTo(add_students.GetPageUrl()));
        }

    }
}