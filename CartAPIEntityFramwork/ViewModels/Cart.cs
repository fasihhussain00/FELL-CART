using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.ViewModels
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid? Customerid { get; set; }
        public DateTimeOffset? CreatedAt { get; set; } 
        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
