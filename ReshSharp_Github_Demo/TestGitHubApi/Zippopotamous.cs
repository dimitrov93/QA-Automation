using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestGitHubApi
{
    public class Zippo
    {
        
        [TestCase("BG","1000","Sofija")]
        [TestCase("BG","8600","Jambol")]
        [TestCase("CA","M5S", "Toronto")]
        public async Task TestZippo(string countryCode, string zipCode, string city)
        {
            // Arrange
            var client = new RestClient("https://api.zippopotam.us");
            var request = new RestRequest(countryCode + "/" +zipCode);

            // Act
            var response = await client.ExecuteAsync(request);
            var location = new SystemTextJsonSerializer().Deserialize<Location>(response);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(countryCode, location.Abbreviation);
            Assert.AreEqual(zipCode, location.PostCode);
            StringAssert.Contains(city, location.Places[0].PlaceName);
        }
    }
}