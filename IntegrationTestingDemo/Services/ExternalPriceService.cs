using System.Text.Json;

namespace IntegrationTestingDemo.Services;

public class ExternalPriceService : IExternalPriceService
{
    private readonly HttpClient _httpClient;

    public ExternalPriceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetProductPriceAsync(int productId)
    {
        var response = await _httpClient.GetAsync($"https://api.example.com/prices/{productId}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<PriceResponse>(json);
        return data?.Price ?? 0;
    }
}

public class PriceResponse
{
    public decimal Price { get; set; }
}