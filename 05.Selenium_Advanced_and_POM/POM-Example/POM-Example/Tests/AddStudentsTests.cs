using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POM_Example.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Example.Tests
{
    public class AddStudentsTests : BaseTests
    {

        [Test]
        public void Test_AddStudentsTests_Url_Heading_Title()
        {
            var add_student = new AddStudentPage(driver);
            add_student.Open();
            Assert.That(driver.Url, Is.EqualTo(add_student.GetPageUrl()));
            Assert.That(add_student.GetPageHeading(), Is.EqualTo("Register New Student"));
            Assert.That(add_student.GetPageTitle(), Is.EqualTo("Add Student"));
        }


        [Test]
        public void Test_AddStudents_HomeLink()
        {
            var add_student = new AddStudentPage(driver);
            add_student.Open();
            add_student.HomeLink.Click();
            var home_page = new HomePage(driver);
            Assert.That(home_page.GetPageTitle(), Is.EqualTo("MVC Example"));
            Assert.That(driver.Url, Is.EqualTo(home_page.GetPageUrl()));
        }


        [Test]
        public void Test_AddStudents_Empty_name_Field()
        {
            var add_student = new AddStudentPage(driver);
            add_student.Open();
            Assert.That(add_student.name.Text, Is.EqualTo(""));
            Assert.That(add_student.name.Text, Is.Empty);
            Assert.IsEmpty(add_student.name.Text);
        }

        [Test]
        public void Test_AddStudents_Empty_email_Field()
        {
            var add_student = new AddStudentPage(driver);
            add_student.Open();
            Assert.That(add_student.email.Text, Is.EqualTo(""));
            Assert.That(add_student.email.Text, Is.Empty);
            Assert.IsEmpty(add_student.email.Text);
        }

        [Test]
        public void Test_AddStudents_Empty_AddButton_IsPresent_And_Clicable()
        {
            var add_student = new AddStudentPage(driver);
            add_student.Open();
            Assert.AreEqual(true, add_student.addButton.Displayed);
            var buttonText = add_student.addButton.Text;
            Assert.AreEqual(buttonText, "Add");
        }

        [Test]
        public void Test_TestAddStudentPage_Links()
        {
            var student = new AddStudentPage(driver);
            var home = new HomePage(driver);
            var view_students = new ViewStudentsPage(driver);
            student.Open();
            student.ViewStudentsLink.Click();
            Assert.That(view_students.GetPageTitle(), Is.EqualTo("Students"));
            student.Open();
            student.HomeLink.Click();
            Assert.That(home.GetPageTitle(), Is.EqualTo("MVC Example"));
            student.Open();
            Assert.That(student.GetPageTitle(), Is.EqualTo("Add Student"));

        }

        [Test]
        public void Test_TestAddStudentPage_AddValidStudent()
        {
            var student = new AddStudentPage(driver);
            student.Open();
            Random rnd = new Random();
            string name = "Student" + rnd.Next();
            string email = "email" + rnd.Next() + "@gmail.com";
            student.CreateStudent(name, email);
            var view_student = new ViewStudentsPage(driver);
            view_student.Open();
            var students = view_student.GetRegisteredStudents();

            foreach (var st in students)
            {
                Assert.IsTrue(st.IndexOf("(") > 0);
                Assert.IsTrue(st.LastIndexOf(")") == st.Length - 1);
            }
        }
    }
}
