using IntegrationTestingDemo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProtectedProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProtectedProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("paginated")]
        public async Task<ActionResult> GetPaginated(int page = 1, int pageSize = 10)
        {
            var products = await _repository.GetAllAsync();
            var paged = products.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(new { Total = products.Count(), Page = page, Products = paged });
        }
    }
}
