using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace TaskBoardSystem
{
    public class TestBoardTests
    {
        private RestClient client;
        private RestRequest request;
        /*https://github.com/nakov/TaskBoard*/
        private const string url = "https://taskboard.dimitrov93.repl.co/api/tasks";

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
        public void Test_First_Task_Is_Called_ProjectSkeleton()
        {
            // Arrange
            this.request = new RestRequest(url);

            // Act
            var response = this.client.Execute(request, Method.Get);
            var data = JsonSerializer.Deserialize<List<Tasks>>(response.Content);

            // Assert
            Assert.That(data.Count, Is.GreaterThan(0));
            Assert.That(data[0].title, Is.EqualTo("Project skeleton"));
        }

        [Test]
        public void Test_Find_Task_By_KeyWord_Home()
        {
            // Arrange
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", "home");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var data = JsonSerializer.Deserialize<List<Tasks>>(response.Content);

            // Assert
            Assert.That(data.Count, Is.GreaterThan(0));
            Assert.That(data[0].title, Is.EqualTo("Home page"));
        }


        [Test]
        public void Test_Find_Task_By_Invalid_KeyWord()
        {
            // Arrange
            string theWord = "missing" + DateTime.Now.Ticks;
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", theWord);

            // Act
            var response = this.client.Execute(request, Method.Get);
            var data = JsonSerializer.Deserialize<List<Tasks>>(response.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsEmpty(data);
            Assert.That(data.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Create_Task_With_Invalid_Data()
        {
            // Arrange
            this.request = new RestRequest(url);

            var body = new
            {
                title = "",
                description = "Api + UI Tests" + DateTime.Now.Ticks,
                board = "Open",
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Title cannot be empty!\"}"));
        }

        [Test]
        public void Test_Create_Valid_Contact_And_Its_In_The_List()
        {
            // Arrange
            this.request = new RestRequest(url);

            var body = new
            {
                title = "Add Tests" + DateTime.Now.Ticks,
                description = "Api + UI Tests" + DateTime.Now.Ticks,
                board = "Open",
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var allTasks = this.client.Execute(request, Method.Get);
            var tasks = JsonSerializer.Deserialize<List<Tasks>>(allTasks.Content);
            var theLastTask = tasks[tasks.Count - 1];
            Assert.That(theLastTask.title, Is.EqualTo(body.title));
            Assert.That(theLastTask.description, Is.EqualTo(body.description));
        }
    }
}