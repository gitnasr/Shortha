using Microsoft.EntityFrameworkCore;
using Shortha.DTO;
using Shortha.Extentions;
using Shortha.Helpers;
using Shortha.Middlewares;
using Shortha.Models;

namespace Shortha
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDependencyInjection();

            builder.Services.AddAppIdentity(config);


            builder.Services.AddIpTracker();
            builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
            builder.Services.AddErrorTransformer();

            builder.Services.AddDbContext<AppDB>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging());


            builder.Services.AddValidation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await DbSeeder.SeedRolesAsync(services);
                await DbSeeder.SeedPackagesAsync(services);
            }
            app.Run();
        }
    }
}
