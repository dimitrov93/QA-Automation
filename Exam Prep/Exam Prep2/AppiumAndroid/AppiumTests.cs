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
        private const string url = "Selenium";
        private const string appLocation = @"C:\com.android.example.github.apk";

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
            var urlField = driver.FindElement(By.Id("com.android.example.github:id/input"));
            urlField.Clear();
            urlField.SendKeys(url);
            driver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);


            var results = driver.FindElements(By.Id("com.android.example.github:id/name"));
            foreach(var result in results)
            {
                if(result.Text == "SeleniumHQ/selenium")
                {
                    result.Click();
                    break;
                }
            };

            var names = driver.FindElements(By.Id("com.android.example.github:id/textView"));

            foreach(var name in names)
            {
                if(name.Text == "barancev")
                {
                    name.Click();
                    break;
                }
            };

            var devName = driver.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"user name\"]"));
            Assert.That(devName.Text, Is.EqualTo("Alexei Barantsev"));
        }

    }
}