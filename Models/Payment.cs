namespace Shortha.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; } = null!;
        public DateTime? PaymentDate { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public string? Currency { get; set; } = null;
        public string? PaymentMethod { get; set; } = null;
        public string? TransactionId { get; set; } = null;
        public string? Status { get; set; } = null;

        public Guid SubscriptionId { get; set; } 
        public virtual Subscription Subscription { get; set; } = null!;
    }
}
