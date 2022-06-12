using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestGitHubApi
{
    public class Tests
    {
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient("https://api.github.com");
            client.Authenticator = new HttpBasicAuthenticator("dimosoftuni", "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");
            var url = "repos/{user}/{repo}/issues";

            this.request = new RestRequest(url, Method.Get);

            request.AddUrlSegment("user", "dimitrov93");
            request.AddUrlSegment("repo", "QA-Automation");
        }

        [Test]
        public async Task Test_Get_Issue()
        {
            var response = await client.ExecuteAsync(request);
            Assert.IsNotNull(response.Content);
            Assert.That(response.StatusCode.ToString() == "OK");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        [Test]
        public async Task Test_Get_Issue2()
        {
            var response = await client.ExecuteAsync(request);
            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

            foreach (var issue in issues)
            {
                Console.WriteLine(issue.id);
                Assert.IsNotNull(issue.id);
                Assert.That(issue.id, Is.GreaterThan(1000000000));
            }
        }

        private async Task<Issue> CreateIssue(string title, string body)
        {
            var req = new RestRequest("https://api.github.com/repos/dimitrov93/QA-Automation/issues");
            request.AddBody(new { body, title });
            var response = await this.client.ExecuteAsync(request, Method.Post);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            return issue;
        }


        [Test]
        public async Task Test_Create_GitHubIssueAsync()
        {
            string title = "Ala Bala";
            string body = "Some body here";

            var issue = await CreateIssue(title, body);
            Console.WriteLine(issue.id);
        }
    }
}