namespace Flash_WebApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string? DescriptionText { get; set; }
        public List<Article> Articles { get; set; }
    }
}
