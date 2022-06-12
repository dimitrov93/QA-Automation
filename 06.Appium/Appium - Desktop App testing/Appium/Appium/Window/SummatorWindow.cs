using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.Window
{
    public class SummatorWindow
    {
        private readonly  WindowsDriver<WindowsElement> driver;

        public SummatorWindow(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }

        public WindowsElement num1 => driver.FindElementByAccessibilityId("textBoxFirstNum");
        public WindowsElement num2 => driver.FindElementByAccessibilityId("textBoxSecondNum");
        public WindowsElement button => driver.FindElementByAccessibilityId("buttonCalc");

        public WindowsElement result => driver.FindElementByAccessibilityId("textBoxSum");

        public string Calculate (string f1, string f2)
        {
            num1.Click();
            num1.SendKeys(f1);

            num2.Click();
            num2.SendKeys(f2);

            button.Click();

            return result.Text;
        }
    }
}
