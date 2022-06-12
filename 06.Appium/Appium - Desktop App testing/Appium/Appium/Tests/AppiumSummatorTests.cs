using Appium.Window;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Appium.Tests
{
    public class AppiumSummatorTests
    {
        private WindowsDriver<WindowsElement> driver;
        // private const string AppiumServer = "http://[::1]:4723/wd/hub";
        private AppiumOptions options;

        private AppiumLocalService appiumLocal;

        [SetUp]
        public void OpenApp()
        {
            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\Cvetomir\Desktop\06.Appium-Desktop-Testing-Exercises-Resources\SummatorDesktopApp.exe");

            appiumLocal = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            appiumLocal.Start();

            /*driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);*/
            driver = new WindowsDriver<WindowsElement>(appiumLocal, options);

/*            var window = new SummatorWindow(driver);
*/
        }

        [TearDown]
        public void ShutDownApp()
        {
            //driver.Quit();
            driver.CloseApp();
            driver.Quit();
            appiumLocal.Dispose();
        }

/*        [Test]
        public void Test_Two_Positive_Numbers()
        {
            var window = new SummatorWindow(driver);
            string result = window.Calculate("5", "15");
            Assert.That(result, Is.EqualTo("20"));
        }

        [Test]
        public void Test_Two_Negative_Numbers()
        {
            var window = new SummatorWindow(driver);
            string result = window.Calculate("-5", "-15");
            Assert.That(result, Is.EqualTo("-20"));
        }

        [Test]
        public void Test_Two_Invalid_Numbers()
        {
            var window = new SummatorWindow(driver);
            string result = window.Calculate("-a", "-b");
            Assert.That(result, Is.EqualTo("error"));
        }*/


        [TestCase("5", "15", "20")]
        [TestCase("-5", "-15", "-20")]
        [TestCase("5", "error", "error")]
        public void Test_Data_Driven(string f1, string f2, string res)
        {
            var window = new SummatorWindow(driver);
            string result = window.Calculate(f1,f2);
            Assert.That(result, Is.EqualTo(res));
        }
    }
}