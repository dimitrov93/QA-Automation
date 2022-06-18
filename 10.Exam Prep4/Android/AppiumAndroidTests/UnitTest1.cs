using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace AppiumAndroidTests
{
    public class Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string ContactBookUrl = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"C:\Users\Cvetomir\Desktop\apps\com.example.androidappsummator.apk";

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
        public void Test_Valid()
        {
            var firstField = driver.FindElement(By.Id("com.example.androidappsummator:id/editText1"));
            var secondField = driver.FindElement(By.Id("com.example.androidappsummator:id/editText2"));
            var result = driver.FindElement(By.Id("com.example.androidappsummator:id/editTextSum"));
            var calcButton = driver.FindElement(By.Id("com.example.androidappsummator:id/buttonCalcSum"));

            firstField.SendKeys("5");
            secondField.SendKeys("5");
            calcButton.Click();
            Assert.That(result.Text, Is.EqualTo("10"));
        }

        [Test]
        public void Test_InValid()
        {
            var firstField = driver.FindElement(By.Id("com.example.androidappsummator:id/editText1"));
            var secondField = driver.FindElement(By.Id("com.example.androidappsummator:id/editText2"));
            var result = driver.FindElement(By.Id("com.example.androidappsummator:id/editTextSum"));
            var calcButton = driver.FindElement(By.Id("com.example.androidappsummator:id/buttonCalcSum"));

            firstField.SendKeys("a");
            secondField.SendKeys("b");
            calcButton.Click();
            Assert.That(result.Text, Is.EqualTo("error"));
        }
    }
}