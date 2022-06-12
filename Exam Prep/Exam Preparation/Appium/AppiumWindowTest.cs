using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;

namespace Appium
{
    public class AppiumWindowTest
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string ContactBookUrl = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"C:\Users\Cvetomir\Desktop\QA Automation\Exam Prep\ContactBook-DesktopClient\ContactBook-DesktopClient.exe";

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
        public void Windows()
        {
            //Arrange
            var urlField = driver.FindElementByAccessibilityId("textBoxApiUrl");
            urlField.Clear();
            urlField.SendKeys(ContactBookUrl);

            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            string windowsName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowsName);

            var textBoxSearch = driver.FindElementByAccessibilityId("textBoxSearch");
            textBoxSearch.SendKeys("steve");

            //Act
            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var element = wait.Until(d =>
            {
                var labelResult = driver.FindElementByAccessibilityId("labelResult").Text;
                return labelResult.StartsWith("Contacts found");
            });


            //Assert
            var labelResult = driver.FindElementByAccessibilityId("labelResult").Text;
            Assert.That(labelResult, Is.EqualTo("Contacts found: 1"));

            // :\"][@AutomationId=\"dataGridViewContacts\"]/Custom[@Name=\"Row 0\"]/Edit[@Name=\"FirstName Row 0, Not sorted.\"]"
            var firstName = driver.FindElement(By.XPath("//Edit[@Name=\"FirstName Row 0, Not sorted.\"]"));
            var lastName = driver.FindElement(By.XPath("//Edit[@Name=\"LastName Row 0, Not sorted.\"]"));

            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }
    }
}