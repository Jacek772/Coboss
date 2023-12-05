using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class RefreshTokenData : BaseEntitiy
    {
        public string Token { get; set; } = default!;
        public string JwtId { get; set; } = default!;
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }

        public User User { get; set; } = default!;

    }
}
