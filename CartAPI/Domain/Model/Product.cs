using System;

namespace CartAPI.Domain.Model
{
    public class Product
    {
        public Guid ID { get; set; } = Guid.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0.0M;
        public decimal Quantity { get; set; } = 0.0M;
        public string Metadata { get; set; } = string.Empty;
    }
}