using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shortha.Domain;

namespace Shortha.Infrastructure.Configurations
{
    public class AppDBFactory : IDesignTimeDbContextFactory<AppDB>
    {
        public AppDB CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
         .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Shortha"))
         .AddJsonFile("appsettings.json")
         .Build();


            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDB>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDB(optionsBuilder.Options);
        }
    }
    public class AppDB : IdentityDbContext<AppUser>

    {
        public DbSet<Url> Urls { get; set; } = null!;
        public DbSet<Visit> Visits { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;

        public DbSet<Package> Packages { get; set; } = null!;

        public AppDB(DbContextOptions<AppDB> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UrlConfiguration());
            modelBuilder.ApplyConfiguration(new VisitConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfigration());
            base.OnModelCreating(modelBuilder);

        }
    }
}
