using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.Models
{
    public partial class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
