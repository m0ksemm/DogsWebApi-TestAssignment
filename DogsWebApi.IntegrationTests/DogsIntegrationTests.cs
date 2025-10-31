using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace DogsWebApi.IntegrationTests
{
    public class DogsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public DogsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RootEndpoint_ShouldReturnNotFound()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
