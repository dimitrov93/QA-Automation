using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace ContactBook
{
    public class Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private WebDriverWait wait;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string ContactBookUrl = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"C:\Users\Cvetomir\Desktop\apps\contactbook-androidclient.apk";

        [SetUp]
        public void OpenApp()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLocation);

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [TearDown]
        public void ShutDownApp()
        {
            driver.Quit();
        }

        [Test]
        public void Testing()
        {
            driver.FindElementById("contactbook.androidclient:id/editTextApiUrl").Clear();
            driver.FindElementById("contactbook.androidclient:id/editTextApiUrl").SendKeys("https://contactbook.nakov.repl.co/api");
            driver.FindElementById("contactbook.androidclient:id/buttonConnect").Click();
            driver.FindElementById("contactbook.androidclient:id/editTextKeyword").SendKeys("Steve");
            driver.FindElementById("contactbook.androidclient:id/buttonSearch").Click();

            var firstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName").Text;
            var lastName = driver.FindElementById("contactbook.androidclient:id/textViewLastName").Text;
            var resultsText = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");

            wait.Until(t => resultsText.Text != "");

            Assert.That(firstName, Is.EqualTo("Steve"));
            Assert.That(lastName, Is.EqualTo("Jobs"));
            Assert.That(resultsText.Text, Is.EqualTo("Contacts found: 1"));

            driver.FindElementById("contactbook.androidclient:id/editTextKeyword").Clear();
            driver.FindElementById("contactbook.androidclient:id/editTextKeyword").SendKeys("e");
            driver.FindElementById("contactbook.androidclient:id/buttonSearch").Click();

            var newResults = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(x => newResults.Text != "");

            var allNames = driver.FindElementsById("contactbook.androidclient:id/textViewFirstName");
            Console.WriteLine(allNames.Count);
            Assert.That(allNames.Count, Is.GreaterThanOrEqualTo(3));

        }
    }
}