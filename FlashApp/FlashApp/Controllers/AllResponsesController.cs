using Flash_WebApplication.Models;
using FlashApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlashApp.Controllers
{
    [ApiController]
    [Route("/api/allResponses")]
    [Produces(TestConfig.Content)]
    public class AllResponsesController : ControllerBase
    {
        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri(TestConfig.BaseAdress),
        };

        private readonly ILogger<AllResponsesController> _logger;
        private readonly IProductService _productService;

        public AllResponsesController(ILogger<AllResponsesController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet(Name = "GetAllResponses")]
        public async Task<IActionResult> Get(double price)
        {
            
            var response =  await httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
            if (response == null)
            {
                _logger.LogError("failed to get list of productData from adress {BaseAddress}", httpClient.BaseAddress);
                return  NoContent();
            }

           List<Product> products = _productService.GetMostExpensiveAndCheapestProductPricePerLiter(response)!;
            products.AddRange(_productService.GetProductsWithDefinedPrice(response, price)!);
            products.Add(_productService.GetProductWithMostBottles(response)!);
            _logger.LogDebug("successfully gathered products");
           return Ok(products);
        }
    }
}
