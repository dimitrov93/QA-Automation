using System;
using System.Collections.Generic;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;

var client = new RestClient("https://api.github.com");
client.Authenticator = new HttpBasicAuthenticator("dimosoftuni", "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");

var url = "repos/{user}/{repo}/issues";

var request = new RestRequest(url);

request.AddUrlSegment("user", "dimitrov93");
request.AddUrlSegment("repo", "QA-Automation");
request.AddBody(new { title = "New Issue from RestSharp" });
var response = await client.ExecuteAsync(request, Method.Post);
Console.WriteLine("Status code " + response.StatusCode);
Console.WriteLine("Body " + response.ResponseUri);

/*var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

foreach (var issue in issues)
{
    Console.WriteLine("Issues number " + issue.number);
    Console.WriteLine("Issues id " + issue.id);
}*/

/*Console.WriteLine("Status code: " + response.StatusCode);
Console.WriteLine("Body: " + response.Content);*/