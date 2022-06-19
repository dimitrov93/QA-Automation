using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace TaskBoardWebAppSelenium
{
    public class SeleniumTests
    {

        private WebDriver driver;
        private const string url = "https://taskboard.nakov.repl.co";

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
        public void Test_List_All_Tasks_And_Check_First_One()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var allTasks = driver.FindElement(By.LinkText("Task Board"));

            // Act
            allTasks.Click();

            // Assert
            var firstTask = driver.FindElement(By.CssSelector("#task1 > tbody > tr.title > td")).Text;
            Assert.That(firstTask, Is.EqualTo("Project skeleton"));
        }

        [Test]
        public void Test_Search_with_Home_Word()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var search = driver.FindElement(By.LinkText("Search"));

            // Act
            search.Click();
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("home");

            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();

            // Assert
            var firstTask = driver.FindElement(By.CssSelector("#task2 > tbody > tr.title > td")).Text;
            var searchResult = driver.FindElement(By.Id("searchResult")).Text;

            Assert.That(firstTask, Is.EqualTo("Home page"));
            Assert.That(searchResult, Is.EqualTo("1 tasks found."));
        }

        [Test]
        public void Test_Search_with_Missing_Word()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var search = driver.FindElement(By.LinkText("Search"));
            var missingWord = "missing" + DateTime.Now.Ticks;

            // Act
            search.Click();
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys(missingWord);

            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();

            // Assert
            var tasksTitle = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            var searchResult = driver.FindElement(By.Id("searchResult")).Text;

            Assert.That(tasksTitle, Is.EqualTo("Tasks Matching Keyword" + " " + "\""+missingWord+"\""));
            Assert.That(searchResult, Is.EqualTo("No tasks found."));
        }

        [Test]
        public void Test_Creating_Task_Invalid_Data()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var search = driver.FindElement(By.LinkText("Create"));

            // Act
            search.Click();
            var description = driver.FindElement(By.Id("description"));
            description.SendKeys("Description" + DateTime.Now.Ticks);
            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();

            // Assert
            var error = driver.FindElement(By.CssSelector("body > main > div")).Text;
            var headerName = driver.FindElement(By.CssSelector("body > header > h1")).Text;
            Assert.That(error, Is.EqualTo("Error: Title cannot be empty!"));
            Assert.That(headerName, Is.EqualTo("Create Task"));
        }

        [Test]
        public void Test_Creating_Task_Valid_Data()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var search = driver.FindElement(By.LinkText("Create"));

            // Act
            search.Click();
            var title = driver.FindElement(By.Id("title"));
            var titleText = "Description" + DateTime.Now.Ticks;
            title.SendKeys(titleText);

            var description = driver.FindElement(By.Id("description"));
            var descriptionText = "Description" + DateTime.Now.Ticks;
            description.SendKeys(descriptionText);

            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();

            // Assert
            var headerName = driver.FindElement(By.CssSelector("body > header > h1")).Text;
            Assert.That(headerName, Is.EqualTo("Task Board"));

            var allTasks = driver.FindElements(By.CssSelector("body > main > div > div:nth-child(1) > table.task-entry"));
            var lastTask = allTasks.Last();

            var resultTitle = lastTask.FindElement(By.CssSelector("tr.title > td")).Text;
            var resultDescription = lastTask.FindElement(By.CssSelector("tr.description > td")).Text;
            Assert.That(resultTitle, Is.EqualTo(titleText));
            Assert.That(resultDescription, Is.EqualTo(descriptionText));
        }
    }
}