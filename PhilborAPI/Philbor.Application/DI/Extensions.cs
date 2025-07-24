using CQRSMediator.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Philbor.Application.Behaviour;
using Philbor.Application.IExternalServices;
using Refit;
using System.Reflection;

namespace Philbor.Application.DI
{
    public static class Extensions
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCQRS(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(EnrichReponseBehavior<,>));
            });

            services.AddRefit(configuration);
        }

        public static void AddRefit(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddRefitClient<IDemoExternalApiService>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ExternalAPIBaseUrls:GetUsers"]!));
        }
    }

}
