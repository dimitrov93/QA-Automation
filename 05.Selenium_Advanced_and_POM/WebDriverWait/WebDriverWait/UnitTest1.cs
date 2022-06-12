using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace WebDriverWaitEx
{
    public class Tests
    {
        private WebDriver driver;
        private WebDriverWait wait;

        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Wait_Thread_Sleep()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://www.uitestpractice.com/Students/Contact");

            var link = driver.FindElement(By.PartialLinkText("This is"));
            link.Click();

            Thread.Sleep(15000);
            var element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotNull(element);
            Assert.IsNotEmpty(element);
        }

        [Test]
        public void Test_Wait_ImplicitWait()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Navigate().GoToUrl("http://www.uitestpractice.com/Students/Contact");

            driver.FindElement(By.PartialLinkText("This is")).Click();

            var element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotNull(element);
            Assert.IsNotEmpty(element);
        }

        [Test]
        public void Test_Wait_ExplicitWait()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            driver.Url = "http://www.uitestpractice.com/Students/Contact";

            driver.FindElement(By.PartialLinkText("This is")).Click();

            var textEl = wait.Until(x =>
            {
                return x.FindElement(By.PartialLinkText("This is")).Text;
            });

            Assert.IsNotNull(textEl);
            Assert.IsNotEmpty(textEl);

            driver.Url = "http://www.uitestpractice.com/Students/Contact";
            driver.FindElement(By.PartialLinkText("This is")).Click();


            var textEl1 = wait.Until(x =>
            {
                return x.FindElement(By.PartialLinkText("This is")).Text;
            });

            Assert.IsNotNull(textEl1);
            Assert.IsNotEmpty(textEl1);

        }

        [Test]
        public void Test_Wait_ExpectedConditions()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            driver.Url = "http://www.uitestpractice.com/Students/Contact";

            driver.FindElement(By.PartialLinkText("This is")).Click();

            var textEl = this.wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("This is")));

            Assert.IsNotNull(textEl);
            Assert.IsNotEmpty(textEl.Text);

            driver.Url = "http://www.uitestpractice.com/Students/Contact";
            driver.FindElement(By.PartialLinkText("This is")).Click();


            var textEl1 = this.wait.Until(
                ExpectedConditions.ElementIsVisible(By.PartialLinkText("This is"))
                );

            Assert.IsNotNull(textEl1);
            Assert.IsNotEmpty(textEl1.Text);

        }
    }
}