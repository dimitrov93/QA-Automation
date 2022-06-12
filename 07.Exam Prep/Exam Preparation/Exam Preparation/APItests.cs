using Exam_Preparation;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ContactBookAPI
{
    public class APItests
    {
        private RestClient client;
        private RestRequest request;
        private const string url = "https://contactbook.nakov.repl.co/api/contacts";

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }

        [Test]
        public void Test_API_is_Working()
        {
            // Arrange
            this.request = new RestRequest(url);

            // Act
            var response = this.client.Execute(request, Method.Get);
           
            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(response.Content);
            Assert.That(response.StatusCode.ToString() == "OK");

        }

        [Test]
        public void Test_First_Contact_Is_Steve_Jobs()
        {
            // Arrange
            this.request = new RestRequest(url);

            // Act
            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            // Assert
            Assert.That(contacts.Count, Is.GreaterThan(0));
            Assert.That(contacts[0].firstName, Is.EqualTo("Steve"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Jobs"));
        }

        [Test]
        public void Test_By_KeyWord_Albert()
        {
            // Arrange
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", "albert");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            // Assert
            Assert.That(contacts[0].firstName, Is.EqualTo("Albert"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Einstein"));
        }

        [Test]
        public void Test_By_Empty_Result()
        {
            // Arrange
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", "missing123412421412");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Create_New_Data_And_Error_Is_Returned()
        {
            // Arrange
            this.request = new RestRequest(url);

            var body = new
            {
                firstName = "Julia",
                lastName = "",
                phone =  "123123131"
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Last name cannot be empty!\"}"));
        }

        [Test]
        public void Test_Create_Valid_Contact_And_Its_In_The_List()
        {
            // Arrange
            this.request = new RestRequest(url);

            var body = new
            {
                firstName = "Julia" + DateTime.Now.Ticks,
                lastName = "Porte",
                email = "julia@abv.bg",
                phone = "123123131"
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);


            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var AllContacts = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(AllContacts.Content);

            var lastContact = contacts[contacts.Count - 1];

/*            var lc = contacts.Last();
*/            /*     
                   Assert.That(lastContact.firstName, Is.EqualTo("Julia"));
                   Assert.That(lastContact.lastName, Is.EqualTo("Porte"));
            */
            Assert.That(lastContact.firstName, Is.EqualTo(body.firstName));
            Assert.That(lastContact.lastName, Is.EqualTo(body.lastName));
        }
    }
}