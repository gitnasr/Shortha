using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shortha.Models.Configuration
{
    public class VisitConfiguration : IEntityTypeConfiguration<Visits>
    {
        public void Configure(EntityTypeBuilder<Visits> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.VisitDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(v => v.UserAgent).IsRequired(false);
            builder.Property(v => v.IpAddress).IsRequired(false);
            builder.Property(v => v.Referrer).IsRequired(false);
            builder.Property(v => v.Country).IsRequired(false);
            builder.Property(v => v.Region).IsRequired(false);
            builder.Property(v => v.City).IsRequired(false);
            builder.Property(v => v.Device).IsRequired(false);
            builder.Property(v => v.Browser).IsRequired(false);
            builder.Property(v => v.Os).IsRequired(false);
            builder.Property(v => v.Count).IsRequired().HasDefaultValue(0);
            builder.HasOne(v => v.Url)
                .WithMany(u => u.Visits)
                .HasForeignKey(v => v.UrlId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
