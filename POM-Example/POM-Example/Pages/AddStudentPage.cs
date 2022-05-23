using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Example.Pages
{
    public class AddStudentPage : BasePage
    {

        public AddStudentPage(IWebDriver driver) : base(driver)
        {
        }

        public override string pageUrl => "https://mvc-app-node-express.nakov.repl.co/add-student";

        public IWebElement name => driver.FindElement(By.Id("name"));
        public IWebElement email => driver.FindElement(By.Id("email"));
        public IWebElement addButton => driver.FindElement(By.CssSelector("body > form > button"));


        public void CreateStudent(string name, string email)
        {
            this.name.SendKeys(name);
            this.email.SendKeys(email);
            this.addButton.Click();
        }

    }
}
