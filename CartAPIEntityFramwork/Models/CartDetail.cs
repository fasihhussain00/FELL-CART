using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.Models
{
    public partial class CartDetail
    {
        public Guid Id { get; set; }
        public Guid? Cartid { get; set; }
        public Guid? Productid { get; set; }
        public decimal? Quantity { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
