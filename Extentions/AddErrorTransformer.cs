using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;

namespace Shortha.Extentions
{
    public static class ErrorTransformer
    {
        public static IServiceCollection AddErrorTransformer(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var response = new ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Validation Failed", errors);

                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
    }
}
