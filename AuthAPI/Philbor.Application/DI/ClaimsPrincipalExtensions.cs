using Philbor.Domain.Shared;
using System.Security.Claims;

namespace Philbor.Application.DI
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.GetUserDataOf(AppConsts.ClaimTypes.UserIdClaim);

            return int.TryParse(userId, out int parsedUserId) ?
                parsedUserId :
                0;
        }

        public static string GetUserDataOf(this ClaimsPrincipal? principal, string claimTypes)
        {
            string? userData = principal?.FindFirst(claimTypes)?.Value;

            return !string.IsNullOrEmpty(userData) ?
                userData :
                string.Empty;
        }

        public static List<string> GetRoles(this ClaimsPrincipal? principal)
        {
            IEnumerable<Claim>? claims = principal?.Claims;

            if (claims == null)
                return [];

            return claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
        }
    }
}
