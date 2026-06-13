using IntegrationTestingDemo.Data;
using IntegrationTestingDemo.Models;
using IntegrationTestingDemo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTestingDemo.Tests.IntegrationTests;

public class ProductRepositoryIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public ProductRepositoryIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AddAndGetAll_Products_ShouldWorkCorrectly()
    {
        // Arrange
        var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        var product = new Product
        {
            Name = "Laptop Test",
            Price = 15000000,
            Stock = 10
        };

        // Act
        await repository.AddAsync(product);
        var products = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(products);
        Assert.Single(products);
        Assert.Equal("Laptop Test", products.First().Name);
    }
}