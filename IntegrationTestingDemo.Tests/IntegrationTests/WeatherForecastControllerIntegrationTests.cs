using System.Net;
using System.Text.Json;
using IntegrationTestingDemo.Models;
using Xunit;

namespace IntegrationTestingDemo.Tests.IntegrationTests;

public class WeatherForecastControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public WeatherForecastControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_WeatherForecast_ReturnsSuccessAndCorrectData()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/WeatherForecast");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Count);
        Assert.All(forecasts, f => Assert.NotNull(f.Summary));
    }
}