using Shortha.Application.Enums;

namespace Shortha.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; } = null!;
        public DateTime? PaymentDate { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddHours(1);
        public decimal Amount { get; set; }
        public string? Currency { get; set; } = null;
        public string? PaymentMethod { get; set; } = null;
        public string? TransactionId { get; set; } = null;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string RefranceId { get; set; } = string.Empty;

        public Guid SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; } = null!;
    }
}
