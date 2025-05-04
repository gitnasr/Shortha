using IPinfo;

namespace Shortha.Extentions
{
    public static class AddIpClient
    {
        public static IServiceCollection AddIpTracker(this IServiceCollection services) {

            services.AddScoped(provider =>
            {
                string token = "5b9b5c5ca0844a"; // Don't do this in production :D 
                return new IPinfoClient.Builder()
                    .AccessToken(token)
                    .Build();
            });
            return services;
        }
    }
}
