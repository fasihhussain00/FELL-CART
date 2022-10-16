using System;

namespace CartAPI.Presentation.ViewModel.Product.Input
{
    public class Product
    {
        public Guid ID { get; set; } = Guid.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0.0M;
        public decimal Quantity { get; set; } = 0.0M;
        public string Metadata { get; set; } = string.Empty;

        public Domain.Model.Product ToDomain()
        {
            return new Domain.Model.Product
            {
                ID = ID,
                Title = Title,
                Category = Category,
                Price = Price,
                Quantity = Quantity,
                Metadata = Metadata
            };
        }
    }
}
