namespace Philbor.Domain.Shared
{
    public class AppConsts
    {
        public static class ClaimTypes
        {
            public const string UserIdClaim = "UserId";
            public const string UserNameClaim = "UserName";
            public const string EmailClaim = "Email";
            public const string UserFullNameClaim = "UserFullName";
        }

        public static class Headers
        {
            public static readonly string AccessToken = "Authorization";
        }
    }
}
