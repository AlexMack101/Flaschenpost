namespace Flash_WebApplication.Models
{
    public class Article 
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }

        public string PricePerUnitText { get; set; }

        public string imageUrl { get; set; }

    }
}
