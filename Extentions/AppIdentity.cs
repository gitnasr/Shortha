using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shortha.Filters;
using Shortha.Models;
using Shortha.Providers;
using System.Text;

namespace Shortha.Extentions
{
    public static class AppIdentity
    {
        private static PasswordOptions PasswordResttictions { get; set; } = new PasswordOptions
        {
            RequireDigit = true,
            RequiredLength = 6,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
        };
        private static SignInOptions SignInOptions { get; set; } = new SignInOptions
        {
            RequireConfirmedAccount = false,
            RequireConfirmedEmail = false,
            RequireConfirmedPhoneNumber = false
        };
        private static LockoutOptions LockoutOptions { get; set; } = new LockoutOptions
        {
            AllowedForNewUsers = true,
            DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15),
            MaxFailedAccessAttempts = 3
        };

     
        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration config)
        {
            return new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"]))
            };
        }


        public static IServiceCollection AddAppIdentity(this IServiceCollection services, IConfiguration config) {

            services.AddIdentity<AppUser, IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Password = PasswordResttictions;
                    options.SignIn=SignInOptions;
                    options.Lockout = LockoutOptions;
                    })
                .AddEntityFrameworkStores<AppDB>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = GetTokenValidationParameters(config) ;
            });
            services.AddSingleton<JwtProvider , JwtProvider>();
            services.AddSingleton<RedisProvider, RedisProvider>(); //TEMP


            services.AddAuthorization(options =>
            {
                options.AddPolicy("NotBlacklisted", policy =>
                    policy.Requirements.Add(new NotBlacklistedRequirement()));

                options.DefaultPolicy = options.GetPolicy("NotBlacklisted")!;
            });

            return services;

        }
    }
}
