using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;

namespace Vivino___Android
{
    public class Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppPackage = "vivino.web.app";
        private const string VivinoAppStartupActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestAccountEmail = "test_vivino@gmail.com";
        private const string VivinoTestAccountPassword = "p@ss987654321";
        private const string ContactBookUrl = "https://contactbook.nakov.repl.co/api";
/*        private const string appLocation = @"C:\Users\Cvetomir\Desktop\apps\vivino.web.app_8.18.11-8181199_minAPI19(arm64-v8a,armeabi,armeabi-v7a,mips,x86,x86_64)(nodpi)_apkmirror.com.apk";
*/
        [SetUp]
        public void OpenApp()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("appPackage", VivinoAppPackage);
            options.AddAdditionalCapability("appActivity", VivinoAppStartupActivity);

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
        }

        [TearDown]
        public void ShutDownApp()
        {
            driver.CloseApp();
            driver.Quit();
        }

        [Test]
        public void Test1()
        {
            var loginButton = driver.FindElement(By.Id("vivino.web.app:id/txthaveaccount"));
            loginButton.Click();

            driver.FindElementById("vivino.web.app:id/edtEmail").SendKeys(VivinoTestAccountEmail);
            driver.FindElementById("vivino.web.app:id/edtPassword").SendKeys(VivinoTestAccountPassword);
            driver.FindElementById("vivino.web.app:id/action_signin").Click();

            driver.FindElementById("vivino.web.app:id/wine_explorer_tab").Click();
            driver.FindElementById("vivino.web.app:id/search_vivino").Click();
            var textinput = driver.FindElementById("vivino.web.app:id/editText_input");
            textinput.Click();
            textinput.SendKeys("Katarzyna Reserve Red 2006" + Keys.Enter);
/*            driver.FindElementById("vivino.web.app:id/wineryname_textView").Click();
*/
            var listSearchResults = driver.FindElementById("vivino.web.app:id/listviewWineListActivity");
            var firstResult = listSearchResults.FindElementByClassName("android.widget.FrameLayout");
            firstResult.Click();

            var wineryName = driver.FindElementById("vivino.web.app:id/winery_name");
            Assert.That(wineryName.Text, Is.EqualTo("Katarzyna"));
            var wineName = driver.FindElementById("vivino.web.app:id/wine_name");
            Assert.That(wineName.Text, Is.EqualTo("Reserve Red 2006"));

            var elementRAting = driver.FindElementById("vivino.web.app:id/rating");
            double rating = double.Parse(elementRAting.Text);
            Assert.IsTrue(rating == 4.7);
            Assert.IsTrue(rating >- 1.0 && rating <= 5.00);
            Assert.Positive(rating);

            /*            var tabsSummary = driver.FindElementById("vivino.web.app:id/tabs");
                        var tabHighlights = tabsSummary.FindElementByXPath("//android.widget.TextView[1]");
                        tabHighlights.Click();*/

            var highlightsDescription = driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"vivino.web.app:id/highlight_description\"))");
            Assert.AreEqual("Among top 1% of all wines in the world", highlightsDescription.Text);

            /*            var tabs = driver.FindElementById("vivino.web.app:id/tabs");
                        var tabsSummary = tabs.FindElementByXPath("//android.widget.TextView[2]");
                        tabsSummary.Click();

                        var wineFactText = driver.FindElementById("vivino.web.app:id/wine_fact_text");
                        Assert.That(wineFactText.Text, Is.EqualTo("Cabernet Sauvignon,Merlot"));
            */
            /*            driver.FindElementByXPath("//android.widget.ImageView[@content-desc=\"More options\"]").Click();
                        driver.FindElementByClassName("android.widget.LinearLayout").Click();*/
        }
    }
}