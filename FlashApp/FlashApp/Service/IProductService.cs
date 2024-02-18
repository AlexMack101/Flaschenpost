using Flash_WebApplication.Models;

namespace FlashApp.Service
{
    public interface IProductService
    {
        public Product? GetProductWithMostBottles(List<Product> response);

        public List<Product>? GetMostExpensiveAndCheapestProductPricePerLiter(List<Product> response);

        public List<Product>? GetProductsWithDefinedPrice(List<Product> response, double price);
    }
}
