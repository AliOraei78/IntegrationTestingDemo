using IntegrationTestingDemo.Data;
using IntegrationTestingDemo.Models;
using IntegrationTestingDemo.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTestingDemo.Tests.IntegrationTests;

public class ProductRepositoryWithTestContainersTests : IClassFixture<TestContainersWebApplicationFactory>, IAsyncLifetime
{
    private readonly TestContainersWebApplicationFactory _factory;

    public ProductRepositoryWithTestContainersTests(TestContainersWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.InitializeAsync();

        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task AddAndRetrieve_Product_ShouldPersistInRealDatabase()
    {
        // Arrange
        var scope = _factory.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        var product = new Product
        {
            Name = "Test Container Laptop",
            Price = 25000000,
            Stock = 5
        };

        // Act
        await repository.AddAsync(product);
        var products = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(products);
        Assert.Contains(products, p => p.Name == "Test Container Laptop");
    }
}