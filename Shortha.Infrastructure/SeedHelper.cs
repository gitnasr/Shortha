using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shortha.Domain;
using Shortha.Infrastructure.Configurations;

namespace Shortha.Helpers
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Premium", "Normal", "Restricted" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedPackagesAsync(IServiceProvider serviceProvider)
        {

            var DBProvider = serviceProvider.GetRequiredService<AppDB>();
            // Check if the packages already exist
            if (DBProvider.Packages.Any())
            {
                return; // Packages already exist, no need to seed
            }
            DBProvider.Packages.Add(new Package
            {
                Name = "Premium",
                price = 5.0m,
                MaxUrls = -1,
                CreatedAt = DateTime.UtcNow,
                Description = "The Premium Package of our Service, Create Unlimted with Usfual insghits and Analytics Service",
            });
            await DBProvider.SaveChangesAsync();
        }
    }
}
