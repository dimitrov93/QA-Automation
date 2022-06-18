using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.PageObjects
{
    public class ViewStudents : Base
    {
        public ViewStudents(IWebDriver driver) : base(driver)
        {
        }

        public override string pageUrl => "https://mvc-app-node-express.dimitrov93.repl.co/students";

        public IWebElement RegisteredStudentsTitle => driver.FindElement(By.CssSelector("body > h1"));

        public IReadOnlyCollection<IWebElement> StudentsCount => driver.FindElements(By.CssSelector("body > ul > li"));

        public string[] GetRegisteredStudents()
        {
            var allStudents = this.StudentsCount.Select(x => x.Text).ToArray();
            return allStudents;
        }
    }
}
