using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace AppiumSummatorMobileTests
{
    public class SummatorTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"C:\Users\Cvetomir\Desktop\QA Automation\com.example.androidappsummator.apk";

        [SetUp]
        public void StartApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            this.driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUri), options);
        }

        [TearDown]
        public void ShutDown()
        {
            this.driver.Quit();
        }

        [TestCase("5","5","10")]
        [TestCase("-5","-5","-10")]
        [TestCase("ab","-5","error")]
        [TestCase("-5","ab","error")]
        [TestCase("","ab","error")]
        public void Test_With_Positive_Values(string f1, string f2, string res)
        {
            driver.FindElementById("com.example.androidappsummator:id/editText1").SendKeys(f1);
            driver.FindElementById("com.example.androidappsummator:id/editText2").SendKeys(f2);
            driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum").Click();
            var result = driver.FindElementById("com.example.androidappsummator:id/editTextSum").Text;
            Assert.That(result, Is.EqualTo(res));
        }
    }
}