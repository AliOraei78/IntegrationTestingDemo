using IntegrationTestingDemo.Data;
using IntegrationTestingDemo.Models;
using IntegrationTestingDemo.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTestingDemo.Tests.IntegrationTests;

[Collection("IntegrationTests")]
public class ProductRepositoryIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;

    public ProductRepositoryIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.InitializeDbAsync();
    }

    public async Task DisposeAsync()
    {
        // Cleanup: Remove all products after each test
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Products.RemoveRange(context.Products);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddProduct_And_GetAll_ShouldReturnAddedProduct()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        var product = new Product
        {
            Name = "Test Laptop Pro",
            Price = 45000000,
            Stock = 15
        };

        // Act
        await repository.AddAsync(product);
        var products = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(products);
        Assert.Single(products); // Because cleanup is executed before each test
        var addedProduct = products.First();
        Assert.Equal("Test Laptop Pro", addedProduct.Name);
        Assert.Equal(45000000, addedProduct.Price);
    }

    [Fact]
    public async Task GetById_AfterAdd_ShouldReturnCorrectProduct()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        var product = new Product
        {
            Name = "Wireless Mouse",
            Price = 1200000,
            Stock = 50
        };

        await repository.AddAsync(product);

        // Act
        var retrieved = await repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(product.Name, retrieved.Name);
        Assert.Equal(product.Price, retrieved.Price);
    }
}