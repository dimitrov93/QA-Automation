using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace ContactBook
{
    public class BookTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"C:\Users\Cvetomir\Desktop\QA Automation\contactbook-androidclient.apk";
        private const string ApiService = "https://contactbook.nakov.rep1.co/api";
        private WebDriverWait wait;

        [SetUp]
        public void StartApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            this.driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [TearDown]
        public void ShutDown()
        {
            this.driver.Quit();
        }



        [Test]
        public void Test_Unknown_Contact()
        {
            driver.FindElementById("contactbook.androidclient:id/buttonConnect").Click();
            driver.FindElementById("contactbook.androidclient:id/editTextKeyword").SendKeys("Gosho");
            var btnSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            btnSearch.Click();
            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(t => result.Text != "");
            Assert.That(result.Text, Is.EqualTo("Contacts found: 0"));
        }


        [Test]
        public void Test_Existing_Contact()
        {
            driver.FindElementById("contactbook.androidclient:id/buttonConnect").Click();
            driver.FindElementById("contactbook.androidclient:id/editTextKeyword").SendKeys("Steve");
            var btnSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            btnSearch.Click();
            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(t => result.Text != "");
            Assert.That(result.Text.Contains("Contacts found: 1"));
        }

        [Test]
        public void Test_List_of_Contacts()
        {
            driver.FindElementById("contactbook.androidclient:id/buttonConnect").Click();
            driver.FindElementById("contactbook.androidclient:id/editTextKeyword").SendKeys("e");
            var btnSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            btnSearch.Click();
            var result = driver.FindElementsById("contactbook.androidclient:id/recyclerViewContacts");
            wait.Until(t => result.Count != 0);
            Assert.That(result.Count > 0);
        }

        [Test]
        public void Test_List_of_Contacts2()
        {
            driver.FindElementById("contactbook.androidclient:id/buttonConnect").Click();
            driver.FindElementById("contactbook.androidclient:id/editTextKeyword").SendKeys("e");
            var btnSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            btnSearch.Click();
            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(t => result.Text != "");
            var names = driver.FindElementsById("contactbook.androidclient:id/textViewFirstName");
            Assert.That(names.Count, Is.EqualTo(3));
        }

    }
}