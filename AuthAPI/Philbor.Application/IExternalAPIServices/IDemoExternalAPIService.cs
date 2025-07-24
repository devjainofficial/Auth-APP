using Philbor.Application.Features.ExternalAPI;
using Refit;

namespace Philbor.Application.IExternalServices
{
    public interface IDemoExternalApiService
    {
        [Get("/users")]
        Task<ApiResponse<List<UserExternalAPIResult>>> GetUsersAsync();

        //If you want to send parameter and bearer token
        //[Get("/users")]
        //Task<ApiResponse<List<UserExternalAPIResult>>> GetUsers(
        //        [Query] GetUserQuery request,
        //        [Header("Authorization")] string bearerToken);
    }
}
