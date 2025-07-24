using Microsoft.AspNetCore.Identity;
using Philbor.Domain.Shared;
using System.Net;

namespace Philbor.Domain.Errors
{
    public class ClientErrors
    {
        #region Erros404
        public static readonly Error UserNotFound = new Error("40401", "User not found.", (int)HttpStatusCode.NotFound);
        #endregion

        #region Erros403
        public static readonly Error UserAccountNotVerified = new Error("40301", "Your account is not verified yet. Please contact to your administrator", (int)HttpStatusCode.Forbidden);
        public static readonly Error EmailAlreadyExists = new Error("40302", "Email already exists.", (int)HttpStatusCode.Forbidden);
        public static readonly Error UserNameAlreadyExists = new Error("40303", "Username already exists.", (int)HttpStatusCode.Forbidden);

        #endregion

        #region Errors423
        public static readonly Error UserLockout = new Error("42301", "Your account is locked out. Please try again later.", (int)HttpStatusCode.Locked);
        #endregion

        #region Errors400
        public static readonly Error InvalidCredentials = new Error("40001", "Credentials not valid", (int)HttpStatusCode.BadRequest);
        public static readonly Error InvalidLoginDetail = new Error("40002", "The login detail is incorrect", (int)HttpStatusCode.BadRequest);
        public static readonly Error InvalidProvider = new Error("40003", "Invalid provider", (int)HttpStatusCode.BadRequest);
        public static readonly Error TokenExpired = new Error("40004", "Token has expired", (int)HttpStatusCode.BadRequest);


        #endregion

        #region Errors500
        public static readonly Error SomethingWentWrong = new Error("50001", "Something Went Wrong", (int)HttpStatusCode.InternalServerError);
        #endregion


        public static Error InvalidToken => new Error("Token.Invalid", "Invalid or expired verification code", (int)HttpStatusCode.NotFound);

        public static Error InternalServerError(string message, int statusCode = (int)HttpStatusCode.InternalServerError)
            => new Error("50020", $"Unexpected Error :- {message}", statusCode);

        public static Error CustomError(CustomException ex)
            => new Error(ex?.Code ?? "500", ex.Message, ex.StatusCode);

        public static Error IdentityError(IEnumerable<IdentityError> errors)
        {
            if (errors.Count() > 0)
            {
                var codes = string.Join(", ", errors.Select(e => $"[{e.Code}]"));
                var descriptions = string.Join(", ", errors.Select(e => $"[{e.Description}]"));

                return new Error(codes, descriptions);
            }

            return SomethingWentWrong;
        }
    }
}
