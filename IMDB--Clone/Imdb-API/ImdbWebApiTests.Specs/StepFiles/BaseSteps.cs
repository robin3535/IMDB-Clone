using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ImdbApi;
using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;
using Xunit;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;

namespace ImdbWebApiTests.Specs.StepFiles
{
    public class BaseSteps
    {
        protected WebApplicationFactory<TestStartup> Factory;
        protected HttpClient Client { get; set; }
        protected HttpResponseMessage Response { get; set; }

        public BaseSteps(WebApplicationFactory<TestStartup> baseFactory)
        {
            // instance of web application factory provided through Dependency Injection
            Factory = baseFactory;
        }
        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
            // Create a test client using the factory instance
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/") //The base address of the test server is http://localhost
            });
        }

        [When(@"I make GET Request '([^']*)'")]
        public virtual async Task WhenIMakeGETRequest(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            Response = await Client.GetAsync(uri);
        }

        [Then(@"response code must be '([^']*)'")]
        public void ThenResponseCodeMustBe(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

        [Then(@"response data must look like from file '([^']*)'")]
        public void ThenResponseDataMustLookLikeFromFile(string filePath)
        {
            var responseData = Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string json;
            using (StreamReader r = new StreamReader(filePath))
            {
                json = r.ReadToEnd();
                
            }

            Assert.Equal(json, responseData);
        }
        [Then(@"response data must look like '([^']*)'")]
        public void ThenResponseDataMustLookLike(string p0)
        {
            var responseData = Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Equal(p0, responseData);
        }

        [When(@"I make GET Request '([^']*)' with parameter '([^']*)'")]
        public virtual async Task WhenIMakeGETRequestWithParameter(string resourceBaseUrl, string parameter)
        {
            var resourceEndpoint = resourceBaseUrl + "/" + parameter;
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            Response = await Client.GetAsync(uri);
        }

        [When(@"I make POST Request '([^']*)' with body '([^']*)'")]
        public virtual async Task WhenIMakePOSTRequestWithBody(string resourceEndpoint, string postDataJson)
        {
            var postRelativeUri = new Uri(resourceEndpoint, UriKind.Relative);
            var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
            Response = await Client.PostAsync(postRelativeUri, content);
        }

        [When(@"I make PUT Request '([^']*)' with parameter '([^']*)' and body '([^']*)'")]
        public virtual async Task WhenIMakePUTRequestWithParameterAndBody(string resourceBaseUrl, string parameter, string putDataJson)
        {
            string resourceEndpoint = resourceBaseUrl + "/" + parameter;
            var postRelativeUri = new Uri(resourceEndpoint, UriKind.Relative);
            var content = new StringContent(putDataJson, Encoding.UTF8, "application/json");
            Response = await Client.PutAsync(postRelativeUri, content);
        }

        [When(@"I make DELETE Request '([^']*)' with parameter '([^']*)'")]
        public virtual async Task WhenIMakeDELETERequestWithParameter(string resourceBaseUrl, string parameter)
        {
            var resourceEndpoint = resourceBaseUrl + "/" + parameter;
            var postRelativeUri = new Uri(resourceEndpoint, UriKind.Relative);
            Response = await Client.DeleteAsync(postRelativeUri);
        }
    }
}
