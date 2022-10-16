using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string Category1 { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
