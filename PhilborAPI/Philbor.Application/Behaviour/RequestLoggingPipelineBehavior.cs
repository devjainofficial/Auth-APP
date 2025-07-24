using CQRSMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Philbor.Domain.Shared;
using Serilog.Context;
using System.Text.Json;

namespace Philbor.Application.Behaviour
{
    internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
     ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : class
     where TResponse : Result
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            string moduleName = GetModuleName(typeof(TRequest).FullName!);
            string requestName = typeof(TRequest).Name;

            using (LogContext.PushProperty("Module", moduleName))
            {
                logger.LogInformation("Processing request {RequestName}", requestName);

                TResponse result = await next();

                if (result.IsSuccess)
                {
                    string response = JsonSerializer.Serialize(result);
                    logger.LogInformation("Completed request {RequestName} with response {response} :- ", requestName, response);
                }
                else
                {
                    using (LogContext.PushProperty("Error", result.Error, true))
                    {
                        logger.LogInformation($"Completed request {requestName} with faillure " + $"[{result.Error.Code} , {result.Error.Description}]");
                    }
                }

                return result;
            }
        }

        private static string GetModuleName(string requestName) => requestName.Split('.')[2];
    }
}
