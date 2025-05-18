namespace e_commerce_website.Models.product
    {
    public class Filter
        {
        public string Category { get; set; } = string.Empty;
        public string Sort { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public double MinPrice { get; set; } = 100;

        public double MaxPrice { get; set; } = 5000;
        }
    }
