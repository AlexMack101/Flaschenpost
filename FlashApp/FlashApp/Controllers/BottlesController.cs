using Flash_WebApplication.Models;
using FlashApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlashApp.Controllers
{
    [ApiController]
    [Route("/api/ProductWithMostBottles")]
    [Produces(TestConfig.Content)]
    public class BottlesController : ControllerBase
    {
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri(TestConfig.BaseAdress),
        };

        private readonly ILogger<BottlesController> _logger;
        private readonly IProductService _productService;

        public BottlesController(ILogger<BottlesController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet(Name = nameof(GetProductWithMostBottles))]
        public async Task<IActionResult> GetProductWithMostBottles()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
                return response != null ? Ok(_productService.GetProductWithMostBottles(response)) : NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError("an error occured while retriving data {message}", e.Message);
                return NotFound();
            }
        }
    }
}
