using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortha.Domain;

namespace Shortha.Infrastructure.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.StartDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            builder.HasOne(s => s.User)
                .WithOne(u => u.Subscription)
                .HasForeignKey<Subscription>(s => s.UserId);

            builder.HasOne(s => s.Payment)
                .WithOne(p => p.Subscription)
                .HasForeignKey<Subscription>(s => s.PaymentId);



        }
    }
}
