using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shortha.Models.Configuration;

namespace Shortha.Models
{
    public class AppDB : IdentityDbContext<AppUser>

    {
        public DbSet<Url> Urls { get; set; } = null!;
        public DbSet<Visit> Visits { get; set; } = null!;

        public AppDB(DbContextOptions<AppDB> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UrlConfiguration());
            modelBuilder.ApplyConfiguration(new VisitConfiguration());
            base.OnModelCreating(modelBuilder);

        }
    }
}
