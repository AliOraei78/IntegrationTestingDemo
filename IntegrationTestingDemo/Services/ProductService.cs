using IntegrationTestingDemo.Models;
using IntegrationTestingDemo.Repositories;

namespace IntegrationTestingDemo.Services;

public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly IExternalPriceService _priceService;
    private readonly IEmailService _emailService;

    public ProductService(
        IProductRepository repository,
        IExternalPriceService priceService,
        IEmailService emailService)
    {
        _repository = repository;
        _priceService = priceService;
        _emailService = emailService;
    }

    public async Task<bool> ProcessOrderAsync(int productId, string customerEmail)
    {
        var product = await _repository.GetByIdAsync(productId);

        if (product == null)
            return false;

        var externalPrice = await _priceService.GetProductPriceAsync(productId);

        // Business logic, e.g., price comparison

        await _emailService.SendOrderConfirmationAsync(
            customerEmail,
            $"Order for product {product.Name}");

        return true;
    }
}