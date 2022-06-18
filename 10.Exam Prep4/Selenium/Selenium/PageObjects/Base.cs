using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.PageObjects
{
    public class Base
    {
        protected readonly IWebDriver driver;
        public virtual string pageUrl { get; }


        public Base(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public IWebElement HomeLink => driver.FindElement(By.XPath("/html/body/a[1]"));
        public IWebElement ViewStudents => driver.FindElement(By.XPath("/html/body/a[2]"));
        public IWebElement AddStudents => driver.FindElement(By.CssSelector("body > a:nth-child(5)"));
        public IWebElement Heading => driver.FindElement(By.CssSelector("body > h1"));

        public void OpenThePage()
        {
            driver.Navigate().GoToUrl(this.pageUrl);
        }

        public bool isOpen()
        {
            return driver.Url == this.pageUrl;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }
        public string GetPageUrl()
        {
            return driver.Url;
        }

        public string GetPageHeading()
        {
            return Heading.Text;
        }
    }
}
