using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace WebDriverWaitExamples
{
    public class WaitTests
    {
        private WebDriver driver;


        [Test]
        public void Test_Wait_Thread_Sleep()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://www.uitestpractice.com/Students/Contact");

            var link = driver.FindElement(By.PartialLinkText("This is"));
            link.Click();

            Thread.Sleep(20000);
            var element = driver.FindElement(By.ClassName("ContactUs")).Text;
            // Assert.IsNotNull(element);
            StringAssert.Contains("Selenium is a portable", element);

            driver.Navigate().GoToUrl("http://www.uitestpractice.com/Students/Contact");

            driver.FindElement(By.PartialLinkText("This is")).Click();

            Thread.Sleep(15000);
            var element1 = driver.FindElement(By.ClassName("ContactUs")).Text;
            StringAssert.Contains("Selenium is a portable", element1);

            driver.Quit();
        }
    }
}