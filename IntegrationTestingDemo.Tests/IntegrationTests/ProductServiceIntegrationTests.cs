using IntegrationTestingDemo.Data;
using IntegrationTestingDemo.Models;
using IntegrationTestingDemo.Repositories;
using IntegrationTestingDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace IntegrationTestingDemo.Tests.IntegrationTests;

// 1. Change fixture to TestContainersWebApplicationFactory and implement IAsyncLifetime
public class ProductServiceIntegrationTests : IClassFixture<TestContainersWebApplicationFactory>, IAsyncLifetime
{
    private readonly TestContainersWebApplicationFactory _factory;
    private readonly Mock<IExternalPriceService> _mockPriceService = new();
    private readonly Mock<IEmailService> _mockEmailService = new();

    public ProductServiceIntegrationTests(TestContainersWebApplicationFactory factory)
    {
        _factory = factory;
    }

    // 2. Initialize the container and create the database schema before running tests
    public async Task InitializeAsync()
    {
        await _factory.InitializeAsync();

        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task ProcessOrder_WithMockedExternalServices_ShouldSucceed()
    {
        // Arrange - Override services using the Testcontainers factory
        var customizedFactory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace external services with mocks
                services.AddSingleton(_mockPriceService.Object);
                services.AddSingleton(_mockEmailService.Object);
            });
        });

        var scope = customizedFactory.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
        var productService = scope.ServiceProvider.GetRequiredService<ProductService>();

        // Prepare test data
        var product = new Product
        {
            Name = "Mocked Laptop",
            Price = 20000000,
            Stock = 3
        };

        await repository.AddAsync(product);

        _mockPriceService
            .Setup(x => x.GetProductPriceAsync(It.IsAny<int>()))
            .ReturnsAsync(22000000);

        _mockEmailService
            .Setup(x => x.SendOrderConfirmationAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await productService.ProcessOrderAsync(
            product.Id,
            "test@example.com");

        // Assert
        Assert.True(result);

        _mockPriceService.Verify(
            x => x.GetProductPriceAsync(product.Id),
            Times.Once);

        _mockEmailService.Verify(
            x => x.SendOrderConfirmationAsync(
                "test@example.com",
                It.IsAny<string>()),
            Times.Once);
    }
}