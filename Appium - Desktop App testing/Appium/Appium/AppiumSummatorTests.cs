using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Appium
{
    public class AppiumSummatorTests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://127.0.0.1:4723/wb/hub";
        private AppiumOptions options;

        [SetUp]
        public void OpenApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\Cvetomir\Desktop\WindowsFormsApp.exe");

            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);

        }

        [TearDown]
        public void ShutDownApp()
        {
            this.driver.Quit();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}