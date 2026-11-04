
using Microsoft.AspNetCore.Mvc;
using SupportTicketingSystem.Application.Interfaces;

namespace SupportTicketingSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveProducts()
        {
            var products = await _productService.GetActiveProductsAsync();

            return Ok(products);
        }
    }
}
