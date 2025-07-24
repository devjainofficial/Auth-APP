using Microsoft.Extensions.Logging;
using Philbor.Domain.Shared;

namespace Philbor.Application.Abstractions
{
    public interface IHelperService
    {
        Task<Result> ReturnResultAsync(Func<Task<Result>> action, ILogger logger);
    }
}
