using Microsoft.AspNetCore.Authorization;
using Shortha.Application;
using Shortha.Domain;
using Shortha.Filters;
using Shortha.Infrastructure.Repository;
using Shortha.Providers;

namespace Shortha.Extentions
{
    public static class AddDependacyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IURL, UrlRepository>();
            services.AddScoped<IVisit, VisitRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<GetUrlValidation>();
            services.AddScoped<AppUser, AppUser>();
            services.AddSingleton<IAuthorizationHandler, NotBlacklistedHandler>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddSingleton<IRedisProvider, RedisProvider>();
            services.AddScoped<PackagesRepository>();
            services.AddScoped<PaymentRepository>();


            return services;
        }
    }
}
