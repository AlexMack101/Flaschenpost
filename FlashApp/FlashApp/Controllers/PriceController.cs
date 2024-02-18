using Flash_WebApplication.Models;
using FlashApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlashApp.Controllers
{

    [ApiController]
    [Route("/api/GetMostExpensiveAndCheapestProductPricePerLiter")]
    [Produces(TestConfig.Content)]
    public class PriceController : ControllerBase
    {
        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri(TestConfig.BaseAdress)
        };

        private readonly ILogger<PriceController> _logger;
        private readonly IProductService _productService;

        public PriceController(ILogger<PriceController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet(Name = "GetMostExpensiveAndCheapestProductPricePerLiter")]
        public async Task<IActionResult> GetMostExpensiveAndCheapestProductPricePerLiter()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
                return response != null
                    ? Ok(_productService.GetMostExpensiveAndCheapestProductPricePerLiter(response))
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
