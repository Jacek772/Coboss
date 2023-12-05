namespace Coboss.Types.DTO
{
    public class AuthenticationResultDTO
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
