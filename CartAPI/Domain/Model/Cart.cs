using System;
using System.Collections.Generic;

namespace CartAPI.Domain.Model
{
    public class Cart
    {
        public Guid ID { get; set; } = Guid.Empty;
        public Customer Customer { get; set; }
        public List<CartDetail> LineItems { get; set; }
    }
}
