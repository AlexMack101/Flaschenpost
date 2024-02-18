using Flash_WebApplication.Models;

namespace FlashApp.Service
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// returns the product with the most bottles in the json
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public Product? GetProductWithMostBottles(List<Product> response)
        {
            return response.Any()
                ? response.MaxBy(p => p.Articles
                    .Max(a => Convert.ToInt32(a.ShortDescription.Substring(0,a.ShortDescription.IndexOf("x", StringComparison.InvariantCulture)))))
                : null;
        }

        /// <summary>
        /// Returns a list with the most expensive and the cheapest beer according to price per liter
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public List<Product>? GetMostExpensiveAndCheapestProductPricePerLiter(List<Product> response)
        {
            if(!response.Any())
                return null;

            var mostExpensiveBeer = response.MaxBy(x => x.Articles
                .Max(a => Convert.ToDouble(a.PricePerUnitText.Substring(1, a.PricePerUnitText.IndexOf("€", StringComparison.InvariantCulture) - 2))));

            var cheapestBeer = response.MinBy(x => x.Articles
                .Min(a => Convert.ToDouble(a.PricePerUnitText.Substring(1, a.PricePerUnitText.IndexOf("€", StringComparison.InvariantCulture) - 2))));

            return new List<Product> { mostExpensiveBeer!, cheapestBeer! };
        }

        /// <summary>
        /// returns a list with all product with the defined price order by price per liter ascending
        /// </summary>
        /// <param name="response"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public List<Product>? GetProductsWithDefinedPrice(List<Product> response, double price)
        {
         return response.Any()? response.FindAll(
                   delegate(Product p)
                   {
                       return p.Articles
                           .Any(a => a.Price.Equals(price));
                   })
               .OrderBy(p => p.Articles.Min(
                   a => Convert.ToDouble(a.PricePerUnitText.Substring(1,
                       a.PricePerUnitText.IndexOf("€", StringComparison.InvariantCulture) - 2)))).ToList()
                 : null;
        }
    }
}
