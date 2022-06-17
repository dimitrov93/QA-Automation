using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace API_Tests
{
    public class API_Tests
    {
        private RestClient client;
        private RestRequest request;
        private const string url = "https://EventsAPI.dimitrov93.repl.co";

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }

        [Test]
        public void Test_API_Working()
        {
            this.request = new RestRequest(url);
            var response = this.client.Execute(this.request, Method.Get);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(response.Content);
            Assert.That(response.StatusCode.ToString() == "OK");
        }

        [Test]
        public void Test_All_Events_Retrieve()
        {
            this.request = new RestRequest(url + "/events");
            var response = this.client.Execute(this.request, Method.Get);
            var data = JsonSerializer.Deserialize<List<Events>>(response.Content);
            Assert.That(data.Count, Is.GreaterThan(5));
            Assert.That(data[0].title, Is.EqualTo("College Basketball: Georgetown at Butler"));
        }

        [Test]
        public void Test_Retrieve_Event_2()
        {
            this.request = new RestRequest(url + "/events/2");
            var response = this.client.Execute(this.request, Method.Get);
            var data = JsonSerializer.Deserialize<Events>(response.Content);
            Assert.That(data.Id, Is.EqualTo("2"));
            Assert.That(data.title, Is.EqualTo("IBM Connect 2016"));
        }

        [Test]
        public void Test_Create_And_Last_Object()
        {
            // post
            this.request = new RestRequest(url + "/events/");
            var body = new
            {
                title = "blq" + DateTime.Now.Ticks,
                url = "blq" + DateTime.Now.Ticks + ".com",
                topics = "tests" + DateTime.Now.Ticks
            };
            request.AddJsonBody(body);
            var response = this.client.Execute(this.request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            // get
            var allEvents = this.client.Execute(request, Method.Get);
            var data = JsonSerializer.Deserialize<List<Events>>(allEvents.Content);
            var lastData = data[data.Count - 1];
            Assert.That(lastData.title, Is.EqualTo(body.title));
            Assert.That(lastData.url, Is.EqualTo(body.url));
            Assert.That(lastData.topics, Is.EqualTo(body.topics));
        }

        [Test]
        public void Test_Create_Then_Delete_Then_Check()
        {
            // post
            this.request = new RestRequest(url + "/events/");
            var body = new
            {
                title = "blq" + DateTime.Now.Ticks,
                url = "blq" + DateTime.Now.Ticks + ".com",
                topics = "tests" + DateTime.Now.Ticks
            };
            request.AddJsonBody(body);
            var response = this.client.Execute(this.request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            // get
            var allEvents = this.client.Execute(request, Method.Get);
            var data = JsonSerializer.Deserialize<List<Events>>(allEvents.Content);
            var lastData = data[data.Count - 1];

            Assert.That(lastData.title, Is.EqualTo(body.title));
            Assert.That(lastData.url, Is.EqualTo(body.url));
            Assert.That(lastData.topics, Is.EqualTo(body.topics));

            // delete
            var lastId = data[data.Count - 1].Id;
            this.request = new RestRequest(url + "/events/" + lastId);
            var theEvent = this.client.Execute(request, Method.Delete);
            Assert.That(theEvent.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // get 
            this.request = new RestRequest(url + "/events/");
            var finalCheck = this.client.Execute(request, Method.Get);
            var finalData = JsonSerializer.Deserialize<List<Events>>(finalCheck.Content);
            var finalLastData = finalData[finalData.Count - 1];
            Assert.That(finalLastData.title, Is.Not.EqualTo(body.title));
            Assert.That(finalLastData.url, Is.Not.EqualTo(body.url));
            Assert.That(finalLastData.topics, Is.Not.EqualTo(body.topics));
        }

        [Test]
        public void Test_Delete_with_invalid_ID()
        {
            this.request = new RestRequest(url + "/events/10000");
            var response = this.client.Execute(this.request, Method.Delete);
            Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content.ToString(), Is.EqualTo("{\"error\":\"Event id not found\"}"));
        }


        [Test]
        public void Test_Create_then_Modify()
        {
            // post
            this.request = new RestRequest(url + "/events/");
            var body = new
            {
                title = "blq" + DateTime.Now.Ticks,
                url = "blq" + DateTime.Now.Ticks + ".com",
                topics = "tests" + DateTime.Now.Ticks
            };
            request.AddJsonBody(body);
            var response = this.client.Execute(this.request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            // get
            var allEvents = this.client.Execute(request, Method.Get);
            var data = JsonSerializer.Deserialize<List<Events>>(allEvents.Content);
            var lastData = data[data.Count - 1];
            Assert.That(lastData.title, Is.EqualTo(body.title));
            Assert.That(lastData.url, Is.EqualTo(body.url));
            Assert.That(lastData.topics, Is.EqualTo(body.topics));

            // modify
            var lastElementId = lastData.Id;
            this.request = new RestRequest(url + "/events/" + lastElementId);
            var newbody = new
            {
                title = "modify",
                url = "modify.modify",
                topics = "modifying" 
            };
            request.AddJsonBody(newbody);
            var modifiedResponse = this.client.Execute(this.request, Method.Put);
            Assert.That(modifiedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Get modified element
            this.request = new RestRequest(url + "/events/" + lastElementId);
            var modifiedResponse2 = this.client.Execute(this.request, Method.Get);
            var modifiedData = JsonSerializer.Deserialize<Events>(modifiedResponse2.Content);
            Assert.That(modifiedData.title, Is.EqualTo(newbody.title));
            Assert.That(modifiedData.url, Is.EqualTo(newbody.url));
            Assert.That(modifiedData.topics, Is.EqualTo(newbody.topics));
        }

        [Test]
        public void Test_Modify_with_invalid_ID()
        {
            this.request = new RestRequest(url + "/events/10000");
            var newbody = new
            {
                title = "modify",
                url = "modify.modify",
                topics = "modifying"
            };
            request.AddJsonBody(newbody);
            var response = this.client.Execute(this.request, Method.Put);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content.ToString(), Is.EqualTo("{\"error\":\"Event id not found\"}"));
        }
    }
}