using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Example.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        public virtual string pageUrl { get;}

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public IWebElement HomeLink => driver.FindElement(By.LinkText("Home"));
        public IWebElement ViewStudentsLink => driver.FindElement(By.LinkText("View Students"));
        public IWebElement AddStrudentsLink => driver.FindElement(By.LinkText("Add Student"));
        public IWebElement PageHeadingLink => driver.FindElement(By.CssSelector("body > h1"));

        public void Open()
        {
            driver.Navigate().GoToUrl(this.pageUrl);
        }

        public bool isOpen()
        {
            return driver.Url == this.pageUrl;
        }

        public string GetPageUrl()
        {
            return driver.Url;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPageHeading()
        {
            return PageHeadingLink.Text;
        }
    }
}
