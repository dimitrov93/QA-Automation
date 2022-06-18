using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.PageObjects
{
    internal class HomePage : Base
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string pageUrl => "https://mvc-app-node-express.dimitrov93.repl.co";
        public IWebElement studentsCount => driver.FindElement(By.CssSelector("body > p > b"));

        public int GetStudentsCount()
        {
            return int.Parse(studentsCount.Text);
        }
    }
}
