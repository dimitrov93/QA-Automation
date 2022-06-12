using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;

namespace AppiumAndroid
{
    public class AndroidTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string ContactBookUrl = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"C:\Users\Cvetomir\Desktop\QA Automation\Exam Prep\contactbook-androidclient.apk";

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
            var urlField = driver.FindElement(By.Id("contactbook.androidclient:id/editTextApiUrl"));
            urlField.Clear();
            urlField.SendKeys(ContactBookUrl);

            var buttonConnect = driver.FindElement(By.Id("contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();


            var textBoxSearch = driver.FindElement(By.Id("contactbook.androidclient:id/editTextKeyword"));
            textBoxSearch.SendKeys("steve");

            //Act
            var buttonSearch = driver.FindElement(By.Id("contactbook.androidclient:id/buttonSearch"));
            buttonSearch.Click();

            var firstName = driver.FindElement(By.XPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.FrameLayout[2]/android.view.ViewGroup/android.widget.LinearLayout/androidx.recyclerview.widget.RecyclerView/android.widget.TableLayout/android.widget.TableRow[3]/android.widget.TextView[2]"));
            var lastName = driver.FindElement(By.XPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.FrameLayout[2]/android.view.ViewGroup/android.widget.LinearLayout/androidx.recyclerview.widget.RecyclerView/android.widget.TableLayout/android.widget.TableRow[4]/android.widget.TextView[2]"));

            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }
    }
}