using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.ViewModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? Category { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public string Metadata { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
