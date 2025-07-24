using Microsoft.AspNetCore.Http;
using Philbor.Application.Abstractions;
using Philbor.Application.DI;
using Philbor.Domain.Shared;

namespace Philbor.Infrastructure.Services
{
    public class UserContextService(IHttpContextAccessor _httpContextAccessor) : IUserContext
    {
        public bool IsAuthenticated => _httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ?? false;

        public int UserId => _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ?? 0;

        public string Email => _httpContextAccessor
            .HttpContext?
            .User
            .GetUserDataOf(AppConsts.ClaimTypes.EmailClaim) ?? string.Empty;

        public string UserName => _httpContextAccessor
            .HttpContext?
            .User
            .GetUserDataOf(AppConsts.ClaimTypes.UserNameClaim) ?? string.Empty;

        public string UserFullName => _httpContextAccessor
            .HttpContext?
            .User
            .GetUserDataOf(AppConsts.ClaimTypes.UserFullNameClaim) ?? string.Empty;

        public DateTime? ExpirationTime
        {
            get
            {
                var expirationClaim = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirst("exp");

                if (expirationClaim != null && long.TryParse(expirationClaim.Value, out long expUnixTime))
                {
                    return DateTimeOffset.FromUnixTimeSeconds(expUnixTime).DateTime;
                }

                return null;
            }
        }

        public string AccessToken => _httpContextAccessor.HttpContext?
            .Request
            .Headers
            .GetAuthToken(AppConsts.Headers.AccessToken) ?? string.Empty;
    }
}
