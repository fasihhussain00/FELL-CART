using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.Models
{
    public partial class Product
    {
        public Product()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? Category { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public string Metadata { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
