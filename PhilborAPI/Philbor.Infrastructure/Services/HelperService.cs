using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Domain.Errors;
using Philbor.Domain.Shared;

namespace Philbor.Infrastructure.Services
{
    public class HelperService : IHelperService
    {
        public async Task<Result> ReturnResultAsync(Func<Task<Result>> action,
            ILogger logger)
        {
            try
            {
                return await action();
            }
            catch (CustomException ex)
            {
                logger.LogError(ex, $"Error :- {ex.Message}");
                return Result.Failure(ClientErrors.CustomError(ex));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error :- {ex.Message}");
                return Result.Failure(ClientErrors.InternalServerError(ex.Message));
            }
        }
    }
}
