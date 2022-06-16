using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Exam_Prep2
{
    public class RestSharpTests
    {
        private RestClient client;
        private RestRequest request;
        private const string url = "https://shorturl-1.dimitrov93.repl.co/api/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(url);
        }

        [Test]
        public void Test_API_Is_Working()
        {
            // Arrange
            this.request = new RestRequest();

            // Act
            var response = this.client.Execute(request, Method.Get);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(response.Content);
            Assert.That(response.StatusCode.ToString() == "OK");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Test]
        public void Test_List_short_URLs()
        {
/*            // Arrange
            this.request = new RestRequest("/urls", Method.Get);

            // Act
            var response = this.client.Execute(request, Method.Get);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var urls = new JsonDeserializer().Deserilize<List<UrlResponse>>(response);
            Assert.IsTrue(urls != null);*/
        }

        [TestCase("https://cnn.com", "cnn3")]
        [TestCase("https://cnn.com", "cnn4")]
        public void Test_Create(string url, string shortUrl)
        {
            // Arrange
            this.request = new RestRequest(url + "urls");

            var body = new
            {
                url,
                shortUrl,
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [TestCase("cnn3")]
        [TestCase("cnn4")]
        public void Test_Delete(string shortUrl)
        {
            // Arrange
            this.request = new RestRequest(url + "urls/" + shortUrl);
            request.AddHeader("Content-Type", "application/json");
 
            // Act
            var response = this.client.Execute(request, Method.Delete);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}