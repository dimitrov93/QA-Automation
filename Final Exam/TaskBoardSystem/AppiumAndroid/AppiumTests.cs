using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace AppiumAndroid
{
    public class AppiumTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string appUrl = "https://taskboard.nakov.repl.co/api";
        private const string appLocation = @"C:\Work\taskboard-androidclient.apk";

        [SetUp]
        public void OpenApp()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLocation);

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void ShutDownApp()
        {
            driver.CloseApp();
            driver.Quit();
        }

        [Test]
        public void Test_Android()
        {
            //Arrange
            var urlField = driver.FindElement(By.Id("taskboard.androidclient:id/editTextApiUrl"));
            urlField.Clear();
            urlField.SendKeys(appUrl);

            var buttonConnect = driver.FindElement(By.Id("taskboard.androidclient:id/buttonConnect"));
            buttonConnect.Click();

            var titleResult = driver.FindElement(By.Id("taskboard.androidclient:id/textViewTitle"));
            Assert.That(titleResult.Text, Is.EqualTo("Project skeleton"));

            //Act
            var addButton = driver.FindElement(By.Id("taskboard.androidclient:id/buttonAdd"));
            addButton.Click();

            var titleInput = driver.FindElement(By.Id("taskboard.androidclient:id/editTextTitle"));
            var titleWord = "TitleTest" + DateTime.Now.Ticks;
            titleInput.SendKeys(titleWord);
            var descriptionInput = driver.FindElement(By.Id("taskboard.androidclient:id/editTextDescription"));
            var descriptionWord = "Description" + DateTime.Now.Ticks;
            descriptionInput.SendKeys(descriptionWord);

            var createButton = driver.FindElement(By.Id("taskboard.androidclient:id/buttonCreate"));
            createButton.Click();

            var searchField = driver.FindElement(By.Id("taskboard.androidclient:id/editTextKeyword"));
            searchField.SendKeys(titleWord);

            var searchButton = driver.FindElement(By.Id("taskboard.androidclient:id/buttonSearch"));
            searchButton.Click();

            var resultsFound = driver.FindElement(By.Id("taskboard.androidclient:id/textViewStatus")).Text;
            var titleFound = driver.FindElement(By.Id("taskboard.androidclient:id/textViewTitle")).Text;
            var desriptionFound = driver.FindElement(By.Id("taskboard.androidclient:id/textViewDescription")).Text;

            Assert.That(resultsFound, Is.EqualTo("Tasks found: 1"));
            Assert.That(titleFound, Is.EqualTo(titleWord));
            Assert.That(desriptionFound, Is.EqualTo(descriptionWord));
        }
    }
}