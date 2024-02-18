using Flash_WebApplication.Models;
using FlashApp;
using FlashApp.Service;
using System.Net.Http.Json;

namespace FlashAppTest;

public class ProductServiceTest
{
    public ProductServiceTest()
    {
        _productService = new ProductService();
    }

    private static HttpClient httpClient = new()
    {
        BaseAddress = new Uri(TestConfig.BaseAdress),
    };

    private readonly IProductService _productService;

    [Fact]
    public async Task GetProductWithMostBottlesTest()
    {
        var response =  await httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
        Assert.NotNull(response);
        var result = _productService.GetProductWithMostBottles(response);
        Assert.NotNull(result);
        var article = result.Articles.First();
        Assert.Equal(24, Convert.ToInt32(article.ShortDescription.Substring(0, article.ShortDescription.IndexOf("x", StringComparison.InvariantCulture))));
    }

    [Fact]
    public async Task GetMostExpensiveAndCheapestProductPricePerLiterTest()
    {
        var response = await httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
        Assert.NotNull(response);
        var result = _productService.GetMostExpensiveAndCheapestProductPricePerLiter(response);
        Assert.NotNull(result);
        var expensive = result.First();
        var article = expensive.Articles.First();
        Assert.Equal(3.66, Convert.ToDouble(article.PricePerUnitText.Substring(1, article.PricePerUnitText.IndexOf("€", StringComparison.InvariantCulture) - 2)));

        var cheapest = result.Last();
        var cheapestArticle = cheapest.Articles.First();
        Assert.Equal(1.00, Convert.ToDouble(cheapestArticle.PricePerUnitText.Substring(1, cheapestArticle.PricePerUnitText.IndexOf("€", StringComparison.InvariantCulture) - 2)));
    }

    [Theory]
    [InlineData(17.99)]
    [InlineData(18.99)]
    [InlineData(16.99)]
    public async Task GetProductsWithDefinedPriceWithValidPriceTest(double price)
    {
        var response = await httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
        Assert.NotNull(response);
        var result = _productService.GetProductsWithDefinedPrice(response,price);
        Assert.NotNull(result);
        foreach (var article in result.SelectMany(product => product.Articles))
        {
            Assert.Equal(price, article.Price);
        }
    }

    [Theory]
    [InlineData(-17.99)]
    [InlineData(67.12)]
    [InlineData(0)]

    public async Task GetProductsWithDefinedPriceWithInvalidPriceTest(double price)
    {
        var response = await httpClient.GetFromJsonAsync<List<Product>>(TestConfig.RequestUri);
        Assert.NotNull(response);
        var result = _productService.GetProductsWithDefinedPrice(response, price);
        Assert.Empty(result!);
        
    }
}