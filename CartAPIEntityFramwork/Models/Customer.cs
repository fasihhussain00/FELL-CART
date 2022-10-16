using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Carts = new HashSet<Cart>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
