using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Selenium_Basics
{
    public class Summator
    {
        private WebDriver driver;
        IWebElement textBoxFirstNum;
        IWebElement textBoxSecondNum;
        IWebElement dropDownOperation;
        IWebElement calcBtn;
        IWebElement resetBtn;
        IWebElement divResult;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            if (!Debugger.IsAttached)
                options.AddArguments("--headless", "--window-size=1920,1200");
            this.driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Url = "https://number-calculator.nakov.repl.co/";

            textBoxFirstNum = driver.FindElement(By.Id("number1"));
            textBoxSecondNum = driver.FindElement(By.Id("number2")); ;
            dropDownOperation = driver.FindElement(By.Id("operation")); ;
            calcBtn = driver.FindElement(By.Id("calcButton")); ;
            resetBtn = driver.FindElement(By.Id("resetButton")); ;
            divResult = driver.FindElement(By.Id("result")); ;
        }

        [TestCase("5", "+", "3", "Result: 8")]
        [TestCase("5", "-", "3", "Result: 2")]
        [TestCase("", "/", "3", "Result: invalid input")]
        public void TestCalculatorWebApp(string num1, string op, string num2, string expectedResult)
        {
            //Arrange
            resetBtn.Click();
            if (num1 != "")
                textBoxFirstNum.SendKeys(num1);
            if (op != "")
                dropDownOperation.SendKeys(op);
            if (num2 != "")
                textBoxSecondNum.SendKeys(num2);

            //Act
            calcBtn.Click();

            //Assert
            Assert.AreEqual(expectedResult, divResult.Text);

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