using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shortha.Domain;

namespace Shortha.Infrastructure.Configurations
{

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
