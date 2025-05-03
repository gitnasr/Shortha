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
            services.AddScoped<GetUrlValidation>();
            services.AddScoped<IAppUser, AppUser>(); //TEMP
            services.AddSingleton<IAuthorizationHandler, NotBlacklistedHandler>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddSingleton<IRedisProvider, RedisProvider>();

            return services;
        }
    }
}
