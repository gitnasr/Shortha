using Microsoft.AspNetCore.Identity;
using Shortha.Models;

namespace Shortha.Helpers
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Preimum", "Normal", "Restricted" };

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
                Name = "Preimim",
                price = 5.0m,
                MaxUrls = -1,
                CreatedAt = DateTime.UtcNow,
                Description = "The Preimim Package of our Service, Create Unlimted with Usfual insghits and Analytics Service",
            });
            DBProvider.SaveChanges();
        }
        }
}
