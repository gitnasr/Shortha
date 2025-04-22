using FluentValidation;
using FluentValidation.AspNetCore;
using Shortha.DTO;

namespace Shortha.Extentions
{
    public static class Validations
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Program>();
            return services;
        }
    }
}
