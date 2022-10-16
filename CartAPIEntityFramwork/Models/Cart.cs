using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public Guid Id { get; set; }
        public Guid? Customerid { get; set; }
        public DateTimeOffset? CreatedAt { get; set; } 
        public bool? IsActive { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
