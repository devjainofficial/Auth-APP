namespace Philbor.Application.Abstractions
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }

        int UserId { get; }

        string Email { get; }

        string UserName { get; }

        string UserFullName { get; }

        DateTime? ExpirationTime { get; }

        string AccessToken { get; }
    }
}
