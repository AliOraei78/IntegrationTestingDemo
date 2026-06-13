using IntegrationTestingDemo.Models;
using IntegrationTestingDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        await _repository.AddAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }
}