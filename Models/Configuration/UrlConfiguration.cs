using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shortha.Models.Configuration
{
    public class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.OriginalUrl).IsRequired();
            builder.Property(u => u.ShortHash).IsRequired().HasMaxLength(10);
            builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.ExpirationDate).IsRequired(false);
            builder.Property(u => u.UserId).IsRequired(false);
            builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(u => u.Visits)
                .WithOne(v => v.Url)
                .HasForeignKey(v => v.UrlId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
