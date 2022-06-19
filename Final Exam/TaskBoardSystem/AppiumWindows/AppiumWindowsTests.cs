using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AppiumWindows
{
    public class AppiumWindowsTests
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string appUrl = "https://taskboard.nakov.repl.co/api";
        private const string appLocation = @"C:\Work\TaskBoard.DesktopClient-v1.0\TaskBoard.DesktopClient.exe";

        [SetUp]
        public void OpenApp()
        {
            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability("app", appLocation);

            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void ShutDownApp()
        {
            driver.CloseApp();
            driver.Quit();
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var urlField = driver.FindElementByAccessibilityId("textBoxApiUrl");
            urlField.Clear();
            urlField.SendKeys(appUrl);

            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            Thread.Sleep(5000);

            var buttonAdd = driver.FindElementByAccessibilityId("buttonAdd");
            buttonAdd.Click();

            var titleWord = "Testing" + DateTime.Now.Ticks;
            var descriptionWord = "Description" + DateTime.Now.Ticks;
            driver.FindElementByAccessibilityId("textBoxTitle").SendKeys(titleWord);
            driver.FindElementByAccessibilityId("textBoxDescription").SendKeys(descriptionWord);

            var buttonCreate = driver.FindElementByAccessibilityId("buttonCreate");
            buttonCreate.Click();

            var textBoxSearch = driver.FindElementByAccessibilityId("textBoxSearchText");
            textBoxSearch.SendKeys(titleWord);

            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();

            Thread.Sleep(5000);

            var statusBox = driver.FindElement(By.XPath("//Text[@Name=\"Task637912295457081221\"][starts-with(@AutomationId,\"ListViewSubItem-\")]\""));
            Assert.That(statusBox.Text, Is.EqualTo("1 tasks loaded"));

        }
    }
}