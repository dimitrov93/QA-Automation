using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_Basics
{
    public class Wikipedia
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test_Search_Wikipedia()
        {
            driver.Url = "https://wikipedia.org";
            driver.FindElement(By.Id("searchInput")).Click();
            driver.FindElement(By.Id("searchInput")).SendKeys("QA");
            driver.FindElement(By.Id("searchInput")).SendKeys(Keys.Enter);

            Assert.AreEqual("https://en.wikipedia.org/wiki/QA", driver.Url);
        }

        [Test]
        public void Test_Summator_Valid_Inputs()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co/";
            driver.FindElement(By.Id("resetButton")).Click();
            driver.FindElement(By.Id("number1")).SendKeys("15");
            driver.FindElement(By.Id("number2")).SendKeys("5");
            driver.FindElement(By.Id("calcButton")).Click();

            var result = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.AreEqual("Sum: 20", result);
            Assert.That(result, Is.EqualTo("Sum: 20"));
        }

        [Test]
        public void Test_Summator_InValid_Inputs()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co/";
            driver.FindElement(By.Id("resetButton")).Click();
            driver.FindElement(By.Id("number1")).SendKeys("Hello");
            driver.FindElement(By.Id("number2")).SendKeys("5");
            driver.FindElement(By.Id("calcButton")).Click();

            var result = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.AreEqual("Sum: invalid input", result);
            Assert.That(result, Is.EqualTo("Sum: invalid input"));
        }


        [Test]
        public void Test_Summator_Valid_Negative_Inputs()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co/";
            driver.FindElement(By.Id("resetButton")).Click();
            driver.FindElement(By.Id("number1")).SendKeys("-5");
            driver.FindElement(By.Id("number2")).SendKeys("-5");
            driver.FindElement(By.Id("calcButton")).Click();

            var result = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.AreEqual("Sum: -10", result);
            Assert.That(result, Is.EqualTo("Sum: -10"));
        }

        [Test]
        public void Test_Summator_Empty_Inputs()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co/";
            driver.FindElement(By.Id("resetButton")).Click();
            driver.FindElement(By.Id("number1")).SendKeys("");
            driver.FindElement(By.Id("number2")).SendKeys("");
            driver.FindElement(By.Id("calcButton")).Click();

            var result = driver.FindElement(By.CssSelector("#result")).Text;
            Assert.AreEqual("Sum: invalid input", result);
            Assert.That(result, Is.EqualTo("Sum: invalid input"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}