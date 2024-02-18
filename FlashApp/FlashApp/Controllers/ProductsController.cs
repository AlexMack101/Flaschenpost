using Flash_WebApplication.Models;
using FlashApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlashApp.Controllers
{
    [ApiController]
    [Route("/api/GetProductsWithDefinedPrice")]
    [Produces(TestConfig.Content)]
    public class ProductsController : ControllerBase
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(TestConfig.BaseAdress),
        };

        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet(Name = "GetProductsWithDefinedPrice")]
        public async Task<IActionResult> GetProductsWithDefinedPrice(double price)
        {
            try
            {
                if (price <= 0)
                {
                    _logger.LogError("failed to get products for a price with the value {price}", price);
                    return NotFound();
                }

                var response = await _httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
                return response != null ?
                    Ok(_productService.GetProductsWithDefinedPrice(response, price))
                 : NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError("an error occured while retriving data {message}", e.Message);
                return NotFound();
            }
        }
    }
}
