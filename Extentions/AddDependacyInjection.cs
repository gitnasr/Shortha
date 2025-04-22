using Shortha.Filters;
using Shortha.Interfaces;
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
            return services;
        }
    }
}
