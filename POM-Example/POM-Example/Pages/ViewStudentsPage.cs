using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Example.Pages
{
    public class ViewStudentsPage : BasePage
    {

        public ViewStudentsPage(IWebDriver driver) : base(driver)
        {
        }

        public override string pageUrl => "https://mvc-app-node-express.nakov.repl.co/students";

        public IWebElement studenCount => driver.FindElement(By.CssSelector("body > p > b"));

    }
}
