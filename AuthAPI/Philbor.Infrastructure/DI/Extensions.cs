using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Philbor.Application.Abstractions;
using Philbor.Domain.Models;
using Philbor.Infrastructure.DataContext;
using Philbor.Infrastructure.Repository;
using Philbor.Infrastructure.Services;

namespace Philbor.Infrastructure.DI
{
    public static class Extensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))); ;

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.AuthenticatorTokenProvider = "email";
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

            services.AddScoped<IUserContext, UserContextService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
        }
    }
}
