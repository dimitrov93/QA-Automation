using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ContactBook.WebDriverTests
{
    public class WebDriverTests
    {
        private WebDriver driver;
        private const string url = "https://contactbook.nakov.repl.co/";

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
        public void Test_ListContact_CheckFirstContact()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var allContacts = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(2) > a"));

            // Act
            allContacts.Click();

            // Assert
            var firstName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.fname > td")).Text;
            var lastName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.lname > td")).Text;

            Assert.That(firstName, Is.EqualTo("Steve"));
            Assert.That(lastName, Is.EqualTo("Jobs"));
        }

        [Test]
        public void Test_Search_Result_By_Word()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Search")).Click();

            // Act
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("Albert");
            driver.FindElement(By.Id("Search")).Click();


            // Assert
            var firstName = driver.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastName = driver.FindElement(By.CssSelector("tr.lname > td")).Text;

            Assert.That(firstName, Is.EqualTo("Albert"));
            Assert.That(lastName, Is.EqualTo("Einstein"));
        }

        [Test]
        public void Test_Search_Result_By_Invalid_Word()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Search")).Click();

            // Act
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("invalid2635");
            driver.FindElement(By.Id("Search")).Click();


            // Assert
            var firstResult = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            var secondResult = driver.FindElement(By.CssSelector("#searchResult")).Text;

            Assert.That(firstResult, Is.EqualTo("Contacts Matching Keyword \"invalid2635\""));
            Assert.That(secondResult, Is.EqualTo("No contacts found."));
        }

        [Test]
        public void Test_Create_Invalid_Data()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Create")).Click();


            driver.FindElement(By.Id("firstName")).SendKeys("Gulia");
            driver.FindElement(By.Id("create")).Click();
            var errorOne = driver.FindElement(By.CssSelector("body > main > div"));
            Assert.That(errorOne.Text, Is.EqualTo("Error: Last name cannot be empty!"));

            driver.FindElement(By.Id("lastName")).SendKeys("Ivanova");
            driver.FindElement(By.Id("create")).Click();
            var errorTwo = driver.FindElement(By.CssSelector("body > main > div"));
            Assert.That(errorTwo.Text, Is.EqualTo("Error: Invalid email!"));

            driver.FindElement(By.Id("email")).SendKeys("blqblq");
            driver.FindElement(By.Id("create")).Click();
            var errorThree = driver.FindElement(By.CssSelector("body > main > div"));
            Assert.That(errorThree.Text, Is.EqualTo("Error: Invalid email!"));
        }

        [Test]
        public void Test_Create_Valid_Data()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Create")).Click();

            var firstName = "Gulia" + DateTime.Now.Ticks;
            var lastName =  "Ivanova" + DateTime.Now.Ticks;
            var emailAddress = "Ivanova" + DateTime.Now.Ticks + "@abv.bg";

            driver.FindElement(By.Id("firstName")).SendKeys(firstName);
            driver.FindElement(By.Id("lastName")).SendKeys(lastName);
            driver.FindElement(By.Id("email")).SendKeys(emailAddress);
            driver.FindElement(By.Id("create")).Click();

            var allContacts = driver.FindElements(By.CssSelector("table.contact-entry"));
            var lastContact = allContacts.Last();

            var firstNameResult = lastContact.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastNameResult = lastContact.FindElement(By.CssSelector("tr.lname > td")).Text;
            var emailNameResult = lastContact.FindElement(By.CssSelector("tr.email > td")).Text;

            Assert.That(firstName, Is.EqualTo(firstNameResult));
            Assert.That(lastName, Is.EqualTo(lastNameResult));
            Assert.That(emailAddress, Is.EqualTo(emailNameResult));

        }
    }
}