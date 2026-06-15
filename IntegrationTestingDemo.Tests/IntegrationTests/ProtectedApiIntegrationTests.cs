using IntegrationTestingDemo.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Xunit;

namespace IntegrationTestingDemo.Tests.IntegrationTests;

public class ProtectedApiIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public ProtectedApiIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.InitializeDbAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Protected_Endpoint_WithValidJwt_ShouldReturnSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        await _factory.InitializeDbAsync();

        // Register a user with a strong password
        var registerModel = new
        {
            Email = "testuser@example.com",
            Password = "Test@123456",
            FullName = "Test User"
        };

        var registerContent = new StringContent(
            JsonSerializer.Serialize(registerModel),
            Encoding.UTF8,
            "application/json");

        var registerResponse = await client.PostAsync("/api/Auth/register", registerContent);

        Assert.True(
            registerResponse.IsSuccessStatusCode,
            $"Registration failed: {await registerResponse.Content.ReadAsStringAsync()}");

        // Login
        var loginModel = new
        {
            Email = "testuser@example.com",
            Password = "Test@123456"
        };

        var loginContent = new StringContent(
            JsonSerializer.Serialize(loginModel),
            Encoding.UTF8,
            "application/json");

        var loginResponse = await client.PostAsync("/api/Auth/login", loginContent);

        Assert.True(
            loginResponse.IsSuccessStatusCode,
            $"Login failed: {await loginResponse.Content.ReadAsStringAsync()}");

        var tokenResponse = await loginResponse.Content.ReadAsStringAsync();

        // Safely extract the token
        var tokenJson = JsonSerializer.Deserialize<JsonElement>(tokenResponse);
        var token = tokenJson.GetProperty("token").GetString();

        Assert.False(string.IsNullOrEmpty(token), "Token is empty");

        // Act - Call the protected endpoint
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var protectedResponse = await client.GetAsync(
            "/api/ProtectedProducts/paginated?page=1&pageSize=5");

        // Assert
        Assert.Equal(HttpStatusCode.OK, protectedResponse.StatusCode);
    }
}