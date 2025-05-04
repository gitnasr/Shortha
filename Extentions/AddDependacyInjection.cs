using Microsoft.AspNetCore.Authorization;
using Shortha.Filters;
using Shortha.Interfaces;
using Shortha.Models;
using Shortha.Providers;
using Shortha.Repository;

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
            services.AddScoped<IAppUser, AppUser>();
            services.AddSingleton<IAuthorizationHandler, NotBlacklistedHandler>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddSingleton<IRedisProvider, RedisProvider>();
            services.AddScoped<PackagesRepository>();
            services.AddScoped<PaymentRepository>();
            services.AddScoped<PaymobProvider>();


            return services;
        }
    }
}
