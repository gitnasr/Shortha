﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortha.Domain;

namespace Shortha.Infrastructure.Configurations
{
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.VisitDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(v => v.UserAgent).IsRequired(false);
            builder.Property(v => v.IpAddress).IsRequired(false);
            builder.Property(v => v.Referrer).IsRequired(false);
            builder.Property(v => v.Country).IsRequired(false);
            builder.Property(v => v.Region).IsRequired(false);
            builder.Property(v => v.City).IsRequired(false);
            builder.Property(v => v.DeviceBrand).IsRequired(false);
            builder.Property(v => v.DeviceType).IsRequired(false);
            builder.Property(v => v.Browser).IsRequired(false);
            builder.Property(v => v.Os).IsRequired(false);
            builder.HasOne(v => v.Url)
                .WithMany(u => u.Visits)
                .HasForeignKey(v => v.UrlId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
