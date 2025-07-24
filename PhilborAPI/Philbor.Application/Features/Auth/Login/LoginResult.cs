namespace Philbor.Application.Features.Auth.Login
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public UserResult User { get; set; }
    }

    public class TokenResult
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public record UserResult
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string Gender { get; set; }
        public required string Email { get; set; }
        public required string LastName { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public IList<string> Roles { get; set; }
    }
}
