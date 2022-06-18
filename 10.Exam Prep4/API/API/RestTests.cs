using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace API
{
    public class RestTests
    {
        private RestClient client;
        private RestRequest request;
        private const string url = "https://contactbook.dimitrov93.repl.co/api/contacts";

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }

        [Test]
        public void Test_If_API_Is_Working()
        {
            this.request = new RestRequest(url);
            var response = this.client.Execute(request, Method.Get);
            Assert.IsNotNull(response);
            Assert.That(response.StatusCode.ToString() == "OK");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Test_If_First_Contact_Is_SteveJobs()
        {
            this.request = new RestRequest(url);
            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            Assert.That(contacts.Count, Is.GreaterThan(0));
            Assert.That(contacts[0].firstName, Is.EqualTo("Steve"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Jobs"));
        }

        [Test]
        public void Test_With_Search_For_Albert()
        {
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", "albert");
            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);
            Assert.That(contacts[0].firstName, Is.EqualTo("Albert"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Einstein"));
        }

        [Test]
        public void Test_With_Search_With_Missing_word()
        {
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", "blqblqblq123");
            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Create_Valid_Data()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Julia" + DateTime.Now.Ticks,
                lastName = "Porte",
                email = "julia@abv.bg",
                phone = "123123131"
            };
            request.AddJsonBody(body);
            var response = this.client.Execute(request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var AllContacts = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contacts>>(AllContacts.Content);
            var lastContact = contacts[contacts.Count - 1];
            Assert.That(lastContact.firstName, Is.EqualTo(body.firstName));
            Assert.That(lastContact.lastName, Is.EqualTo(body.lastName));
        }

        [Test]
        public void Test_Create_Invalid_Data()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Julia" + DateTime.Now.Ticks,
                lastName = "",
                email = "julia@abv.bg",
                phone = ""
            };
            request.AddJsonBody(body);
            var response = this.client.Execute(request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Last name cannot be empty!\"}"));

        }
    }
}