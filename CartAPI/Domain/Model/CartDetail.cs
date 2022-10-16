using System;

namespace CartAPI.Domain.Model
{
    public class CartDetail
    {
        public Guid ID { get; set; } = Guid.Empty;
        public Product Product { get; set; }
        public decimal Quantity { get; set; } = 0.0M;

    }
}
