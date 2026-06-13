namespace IntegrationTestingDemo.Services;

public interface IExternalPriceService
{
    Task<decimal> GetProductPriceAsync(int productId);
}