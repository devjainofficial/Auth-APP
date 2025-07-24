using CQRSMediator.Abstractions;
using Philbor.Domain.Shared;
using System.Diagnostics;

namespace Philbor.Application.Behaviour
{
    public class EnrichReponseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;

        public EnrichReponseBehavior()
        {
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request,
                                            RequestHandlerDelegate<TResponse> next,
                                            CancellationToken cancellationToken)
        {
            DateTime startTime = DateTime.UtcNow;
            _timer.Start();

            TResponse? response = await next(); // Call the next handler in the pipeline

            _timer.Stop();

            long elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (response is Result responseValue)
            {
                responseValue.MetaData = new MetaData(elapsedMilliseconds, startTime);
            }

            return response;
        }
    }
}
