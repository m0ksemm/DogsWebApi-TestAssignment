using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace DogsWebApi.ControllerTests
{
    public class DogsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public DogsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllDogs_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/dogs");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
