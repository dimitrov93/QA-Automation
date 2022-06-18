using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.PageObjects
{
    public class AddStudents : Base
    {
        public AddStudents(IWebDriver driver) : base(driver)
        {
        }

        public override string pageUrl => "https://mvc-app-node-express.dimitrov93.repl.co/add-student";
        public IWebElement nameField => driver.FindElement(By.Id("name"));
        public IWebElement emailField => driver.FindElement(By.Id("email"));
        public IWebElement submitButton => driver.FindElement(By.CssSelector("body > form > button"));

        public void CreateStudent(string name, string email)
        {
            this.nameField.SendKeys(name);
            this.emailField.SendKeys(email);
            this.submitButton.Click();
        }
    }
}
