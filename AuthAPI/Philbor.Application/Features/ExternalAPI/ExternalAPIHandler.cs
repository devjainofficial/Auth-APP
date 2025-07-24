using CQRSMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Application.IExternalServices;
using Philbor.Domain.Errors;
using Philbor.Domain.Shared;
using System.Net;
using System.Text.Json;

namespace Philbor.Application.Features.ExternalAPI
{

    public class ExternalAPIHandler(IDemoExternalApiService demoExternalApiService,
        ILogger<ExternalAPIHandler> logger,
        IHelperService helperService) : IRequestHandler<ExternalAPIQuery, Result>
    {
        public async Task<Result> HandleAsync(ExternalAPIQuery query, CancellationToken cancellationToken)
        {
            return await helperService.ReturnResultAsync(async () =>
            {
                var response = await demoExternalApiService.GetUsersAsync();

                if (response.IsSuccessStatusCode)
                    return Result.Success(response.Content, "User Fetched Successfully");

                var error = JsonSerializer.Deserialize<ApiErrorResponse>(response?.Error?.Content!);
                logger.LogError(response?.Error?.Content, $"Error :- {error?.ExceptionMessage}");
                return Result.Failure(new Error("500", error?.ExceptionMessage!, (int)HttpStatusCode.InternalServerError));

            }, logger);
        }
    }
}
