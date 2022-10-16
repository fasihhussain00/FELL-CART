using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.ViewModels
{
    public class CartDetail
    {
        public Guid Id { get; set; }
        public Guid? Cartid { get; set; }
        public Guid? Productid { get; set; }
        public decimal? Quantity { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
