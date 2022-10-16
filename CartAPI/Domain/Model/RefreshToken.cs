using System;

namespace CartAPI.Domain.Model
{
    public class RefreshToken
    {
        public Guid ID { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        public string Token { get; set; } = string.Empty;
        public string JwtId { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
        public bool IsRevoked { get; set; } = false;
        public DateTimeOffset ExpiryDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
