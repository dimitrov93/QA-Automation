using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace WindowTests
{
    public class WIndowTests
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string appLocation = @"C:\Users\Cvetomir\Desktop\06.Appium-Desktop-Testing-Exercises-Resources\SummatorDesktopApp.exe";

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
        public void Test_Valid_Numbers()
        {
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            var results = driver.FindElementByAccessibilityId("textBoxSum");

            firstField.SendKeys("5");
            secondField.SendKeys("5");
            calcButton.Click();
            Assert.That(results.Text, Is.EqualTo("10"));
        }

        [Test]
        public void Test_Invalid_Numbers()
        {
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            var results = driver.FindElementByAccessibilityId("textBoxSum");

            firstField.SendKeys("a");
            secondField.SendKeys("b");
            calcButton.Click();
            Assert.That(results.Text, Is.EqualTo("error"));
        }
    }
}