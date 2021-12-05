using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MQTT.Api.Contracts.v1.Request.AccountController;
using MQTT.Api.Contracts.v1.Response.AccountController;
using MQTT.Api.Options;
using Xunit;

namespace MQTT.IntegrationTests
{
    public class AccountControllerTests : IntegrationTest
    {
        [Fact]
        public async Task AuthWithInvalidCredentialsWhyDoesNotContainsInDatabase_ReturnBadRequest()
        {
            var request = new UserLoginRequest()
            {
                Email = "arcex.off2@gmail.com",
                Password = "123456",
            };
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Account.Login, request);
            var content = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            content.Should().Be("Invalid credentials.");
        }

        [Fact]
        public async Task AuthWithInvalidCredentialsWithBadEmail_ReturnBadRequest()
        {
            var request = new UserLoginRequest()
            {
                Email = "test",
                Password = "123456",
            };
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Account.Login, request);
            var content = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await response.Content.ReadAsStringAsync()).Should().Contain("Invalid Email Address");
        }
        [Fact]
        public async Task AuthWithInvalidCredentialsWithEmptyEmail_ReturnBadRequest()
        {
            var request = new UserLoginRequest()
            {
                Password = "123456",
            };
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Account.Login, request);
            var content = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await response.Content.ReadAsStringAsync()).Should().Contain("The email address is required");
        }
        [Fact]
        public async Task AuthWithInvalidCredentialsWithEmptyPassword_ReturnBadRequest()
        {
            var request = new UserLoginRequest()
            {
                Email = "arcex.off@gmail.com",
            };
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Account.Login, request);
            var content = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            (await response.Content.ReadAsStringAsync()).Should().Contain("The password is required");
        }
    }
}